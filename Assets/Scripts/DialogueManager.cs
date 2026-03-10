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
    
    public bool dialogueIsPlaying {get; private set;}
    public bool choiceIsActive {get; private set;}
    
    private Story _currentStory;
    private static DialogueManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("More than one DialogueManager in the scene");
        }
        _instance = this;
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

        ContinueDialogue();
    }
    
    private void ContinueDialogue()
    {
        if (!choiceIsActive)
        {
            if (_currentStory.canContinue)
            {
                dialogueText.text = _currentStory.Continue();
                DisplayChoices();
            }
            else
            {
                ExitDialogue();
            }
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
            choiceIsActive = true;
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
}