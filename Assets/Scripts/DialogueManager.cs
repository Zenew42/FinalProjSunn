using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    [Header("DialogueUI")] 
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("ChoicesUI")] 
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] _choicesText;
    public bool choiceIsPlaying;

    [Header("TypingParameters")] 
    [SerializeField] private float _typeSpeed = 0.04f;

    [Header("LoadJSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    public bool dialogueIsPlaying {get; private set;}
    private bool _canContinueToNextLine = false;

    private Story _currentStory;
    private static DialogueManager _instance;
    private Coroutine displayLineCoroutine;
    
    private DialogueVariables _dialogueVariables;
    
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
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);
        _dialogueVariables.StartListening(_currentStory);

        /*
        if (!choiceIsPlaying)
        {
            ContinueDialogue();
        }*/
        
    }
    
    private void ContinueDialogue()
    {
        if (_currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            
            displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));
            //dialogueText.text = _currentStory.Continue();
            
        }
        else
        {
            ExitDialogue();
        }
    }
    
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        
        HideChoices();
        _canContinueToNextLine = false;
        
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(_typeSpeed);
        }
        
        DisplayChoices();
        _canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
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
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }
    
    private void ExitDialogue() 
    {
        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        dialogueText.text = "";
        PauseController.SetPause(false);
        _dialogueVariables.StopListening(_currentStory);
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    
    public void MakeChoice(int choiceIndex)
    {
        if (_canContinueToNextLine)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            choiceIsPlaying = true;
        
            ContinueDialogue();
        }
    }
}