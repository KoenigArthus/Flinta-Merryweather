using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    //[SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    //[SerializeField] private Text displayNameText;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private Text[] choicesText;


    private Story currentStory;

    private static DialogueManager instance;


    public bool dialogueIsPlaying { get; private set; }
    public bool choicesEnabled = false;

    private const string SPEAKER_TAG = "speaker";
    private const string LAYOUT_TAG = "layout";
    private const string STATE_TAG = "state";

 

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        //dialoguePanel.SetActive(false);
        choicesText = new Text[choices.Length];


        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<Text>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();

        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        
        if (dialogueIsPlaying == false)
        {
            currentStory = new Story(inkJSON.text);
            //dialoguePanel.SetActive(true);
            ContinueStory();

        }
        dialogueIsPlaying = true;
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        //dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {

            dialogueText.text = currentStory.Continue();

            DisplayChoices();
            TagHandler(currentStory.currentTags);

        }
        else if (!currentStory.canContinue && !choicesEnabled)
        {

            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (!currentStory.canContinue)
        {
            choicesEnabled = true;
        }

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choiches than possible");

        }


        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }



        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        choicesEnabled = false;

    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    public void TagHandler(List<string> currentTags)
    {

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    ChangeSpeechTextToSpeakerPos(tagValue);
                    break;   
                case LAYOUT_TAG:
                    Debug.Log(tagValue);
                       break; 
                case STATE_TAG:
                    Debug.Log(tagValue);
                    choicesEnabled = false;
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void ChangeSpeechTextToSpeakerPos(string pspeaker)
    {
        switch (pspeaker)
        {
            case "f":
                Debug.Log("Flinta");
                break;
            case "F":
                Debug.Log("Flinta");
                break;
            case "c":
                Debug.Log("character");
                break;
            case "C":
                Debug.Log("character");
                break;
            default:
                Debug.LogError("The case for this Speaker Tag is not defined");
                break;
        }
    }

}
