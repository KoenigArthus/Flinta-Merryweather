using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;



public class DialogueManager : MonoBehaviour
{
    public bool choicesEnabled = false;

    [Header("Dialogue UI")]
    //[SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject uiInventory;
    [SerializeField] private float yOffset = 0.8f;
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject choicespanel;
    [SerializeField] private Text[] choicesText;

    private Controller controller;
    private GameObject speakingCharacter;
    private Story currentStory;
    private Animator ButtonPopUp;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string LAYOUT_TAG = "layout";
    private const string STATE_TAG = "state";

    #region Initialization

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        choicespanel.SetActive(false);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        choicesText = new Text[choices.Length];

        ButtonPopUp = choicespanel.GetComponent<Animator>();

        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<Text>();
            index++;
        }

        controller = gameObject.GetComponent<Controller>();
    }

    #endregion

    #region manage Dialogue
    public void EnterDialogueMode(TextAsset inkJSON, GameObject pcharacter)
    {
            controller.talkingState.dialogueIsPlaying = true;
            choicespanel.SetActive(true);
            uiInventory.SetActive(false);
            speakingCharacter = pcharacter;
            controller.playerMovement.isMoving = false;
            currentStory = new Story(inkJSON.text);
            ContinueStory();
    }

    private void ExitDialogueMode()
    {
        uiInventory.SetActive(true);
        controller.talkingState.dialogueIsPlaying = false;
        dialogueText.text = "";

    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {

            dialogueText.text = currentStory.Continue();
            ButtonPopUp.Play("ChoiceButtonDefault");
            DisplayChoices();
            TagHandler(currentStory.currentTags);

        }
        else if (!currentStory.canContinue && !choicesEnabled)
        {
            ExitDialogueMode();
        }
    }

    

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        choicesEnabled = false;
        ButtonPopUp.SetBool("choicesEnabled", false);
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

    #endregion

    #region Custom Commands (private)

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > 0)
        {
            choicesEnabled = true;
            ButtonPopUp.SetBool("choicesEnabled", true);
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
    }

    //Sets the Speech Text to the positon of the given Ink Tag / string c is the speaking Character, f is the player_Character aka. Flinta
    private void ChangeSpeechTextToSpeakerPos(string pspeaker)
    {
        switch (pspeaker)
        {
            case "f":
                this.ChangeSpeechTextPos(controller.player, yOffset);
                break;
            case "F":
                this.ChangeSpeechTextPos(controller.player, yOffset);
                break;
            case "c":
                this.ChangeSpeechTextPos(speakingCharacter, yOffset);
                break;
            case "C":
                this.ChangeSpeechTextPos(speakingCharacter, yOffset);
                break;
            default:
                Debug.LogError("The case for this Speaker Tag is not defined");
                break;
        }
    }


    //Changes the Speech Text position to that of an given GameObject with an float y Offset
    private void ChangeSpeechTextPos(GameObject pobject, float pyOffset)
    {
        Vector3 lnewTextPos = new Vector3(pobject.transform.position.x, pobject.transform.position.y + pyOffset, 0);
        dialogueText.transform.position = Camera.main.WorldToScreenPoint(lnewTextPos);
    }

#endregion
}
