using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private PlayerMovement _player;
    
    [Header("DialogueUI")]   
    [SerializeField] private float charactersPerSecond = 20f;
    [SerializeField] private float punctuationDelay = 0.5f;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    
    private float baseDelay => 1f / charactersPerSecond;
    
    [Header("ChoicesUI")] 
    [SerializeField] private GameObject[] choices;
    
    [Header("DialogueObjects")]
    [SerializeField] private GameObject[] dialogueObject;
    [SerializeField] private GameObject partyHatObject;
    [SerializeField] private GameObject doorLeaveObject;
    [SerializeField] private InteractionDetector interactionDetector;

    
    private TextMeshProUGUI[] _choicesText;
    
    public bool dialogueIsPlaying {get; private set;}
    
    private Coroutine _displayLineCoroutine;
    public static bool CanContinueToNextLine = true;
    
    
    
    public bool choiceIsActive {get; private set;}
    private Story _currentStory;
    private bool _isTyping;
    public static bool SkippingDialogue;
    private static DialogueManager _instance;
    
    private readonly char[] _punctuationMarks =
    {
        '.', ',', '?', '!', ':', ';', '-'
    };

    [Header("CutscenesRelated")]
    private CutsceneManager _cutsceneManager;
    [SerializeField] private PlayableAsset[] Cutscenes;
    [SerializeField] private GameObject stormyWindow;
    
    [Header("Global Variables")]
    private DialogueVariables _dialogueVariables;
    [SerializeField] private TextAsset loadGlobalsJSON;
    
    [Header("Audio")]
    [SerializeField] private AudioSource[] audioSources;



    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("More than one DialogueManager in the scene");
        }
        _instance = this;
        
        _dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;

        _cutsceneManager = GetComponent<CutsceneManager>();
        _player = FindAnyObjectByType<PlayerMovement>();
        
        _choicesText= new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            _choicesText[index]= choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void StartDialogue(TextAsset inkJson)
    {
        if (dialogueIsPlaying)
        {
            ContinueDialogue();
            return;
        }
        
        _currentStory = new Story(inkJson.text);

        #region External Functions Setup
        _currentStory.BindExternalFunction("PartyHatPick", PartyHat);
        
        _currentStory.BindExternalFunction("PlayMusic", PlayMusic);
        
        _currentStory.BindExternalFunction("TableScene", TableScene);
        _currentStory.BindExternalFunction("DiaryScene", DiaryScene);
        _currentStory.BindExternalFunction("GoOutsideScene", GoOutsideScene);
        _currentStory.BindExternalFunction("PrepareCake", PrepareCake);
        
        _currentStory.BindExternalFunction("PlayThunder", PlayThunder);
        _currentStory.BindExternalFunction("StopPlaying", StopPlaying);
        
        _currentStory.BindExternalFunction("DoorToLeave", SummonDoor);
        _currentStory.BindExternalFunction("CrashGame", ExitGame);
        #endregion
        
        
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);
        
        _dialogueVariables.StartListening(_currentStory);

        ContinueDialogue();
    }
    
    private void ContinueDialogue()
    {
        if (!choiceIsActive)
        {
            if (_currentStory.canContinue)
            {
                if (_displayLineCoroutine != null)
                {
                    StopCoroutine(_displayLineCoroutine);
                }         
                _displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue())); 
            }
            else
            {
                ExitDialogue();
            }
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        CanContinueToNextLine = false; 
        bool isAddingRichTextTag = false;
        HideChoices();
        
        foreach (char letter in line)
        {
            if (SkippingDialogue)
            {
             dialogueText.maxVisibleCharacters = line.Length;
             CanContinueToNextLine = true;  
             SkippingDialogue = false;
             break;
            }
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            } 
            else
            {
                dialogueText.maxVisibleCharacters++;
                
                float delay = _punctuationMarks.Contains(letter)
                    ? punctuationDelay
                    : baseDelay;

                yield return new WaitForSeconds(delay);
            } 
        }
            
        DisplayChoices();
        CanContinueToNextLine = true;
    }
    
    
    private void DisplayChoices()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("Not enough choices. Current choice count:"  + currentChoices.Count);
        }
        
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
            choiceIsActive = true;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
        
        StartCoroutine(SelectFirstChoice());
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }
    
    public void ExitDialogue() 
    {
        _dialogueVariables.StopListening(_currentStory);
        
        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        dialogueText.text = "";
        PauseController.SetPause(false);
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        choiceIsActive = false;
        ContinueDialogue();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        _dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    

    #region External Functions
    void PartyHat()
    {
        Debug.Log("Party Hat picked up");
        
        foreach (GameObject obj in dialogueObject)
            obj.SetActive(true);
        
        partyHatObject.SetActive(false);
        _player.hasHat = true;
        ExitDialogue();
    }

    void PlayMusic()
    {
        Debug.Log("Playing music");
        audioSources[2].Play();
    }

    //Sitting down at the table
    void TableScene()
    {
        //ExitDialogue(); forcing it to exit doesn't let it save the plushies collected VAR which means you can retrigger this cutscene and get softlocked
        interactionDetector.canAct = false;
        Debug.Log("canAct =" + interactionDetector.canAct);
        _cutsceneManager.StartCutscene(Cutscenes[0]);
        Debug.Log(Cutscenes[0].name + " is called");
        StartCoroutine(DelayedAction(12));
    }
    
    //Opening the diary
    void DiaryScene()
    {
        //_player.animator = _player.hatAnimator;
        _cutsceneManager.StartCutscene(Cutscenes[1]);
        Debug.Log(Cutscenes[1].name + " is called");
    }

    void PrepareCake()
    {
        _player.tableDialogue.SetActive(false);
        _cutsceneManager.StartCutscene(Cutscenes[2]);
        Debug.Log(Cutscenes[2].name + " is called");
    }

    //When going out into the void
    void GoOutsideScene()
    {
        interactionDetector.canAct = false;
        _cutsceneManager.StartCutscene(Cutscenes[3]);
        Debug.Log(Cutscenes[3].name + " is called");
        StartCoroutine(DelayedAction(12));
    }

    void PlayThunder()
    {
        audioSources[0].Play();
        audioSources[1].Play();
        stormyWindow.SetActive(true);
       
    }

    void StopPlaying()
    {
        ExitDialogue();
        _cutsceneManager.StopCutscene();
        Debug.Log("Stopped playing cutscene");
    }
    
    public void StartHappyMusic()
    {
        audioSources[2].Play();
    }

    public void StopMusic()
    {
        audioSources[2].Stop();
    }
    
    public void LoadVoid()
    {
        SceneManager.LoadScene("TheVoid");
        Debug.Log("Going Outside");
    }

    void SummonDoor()
    {
        Debug.Log("Door openable");
        
        foreach (GameObject obj in dialogueObject)
            obj.SetActive(false);
        
        doorLeaveObject.SetActive(true);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    
    IEnumerator DelayedAction(float time) {
        Debug.Log("Starting delay...");
        yield return new WaitForSeconds(time); 
        interactionDetector.canAct = true;
    }
}