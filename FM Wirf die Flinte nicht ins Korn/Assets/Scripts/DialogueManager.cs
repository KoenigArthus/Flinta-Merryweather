using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using TMPro;



public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject uiInventory;
    [SerializeField] private float yOffset = 0.8f;
    private Animator ButtonPopUp;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject choicespanel;
    [SerializeField] private TMP_Text[] choicesText;

    [SerializeField] private SceneInfo sceneInfo;


    private GameObject player;
    private GameObject speakingCharacter;
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

        choicespanel.SetActive(false);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        choicesText = new TMP_Text[choices.Length];


        ButtonPopUp = choicespanel.GetComponent<Animator>();

        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TMP_Text>();
            index++;
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void EnterDialogueMode(TextAsset inkJSON, GameObject pcharacter)
    {
        
        if (dialogueIsPlaying == false)
        {
            choicespanel.SetActive(true);
           // uiInventory.SetActive(false);
            speakingCharacter = pcharacter;
            player.GetComponent<Player_Movement>().isMoving = false;
            currentStory = new Story(inkJSON.text);
            ContinueStory();

        }
        dialogueIsPlaying = true;
    }

    private void ExitDialogueMode()
    {
       // uiInventory.SetActive(true);
        dialogueIsPlaying = false;
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
                    if (tagValue == "regina")
                    {
                        sceneInfo.Regina = true;
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    //Sets the Speech Text to the positon of the given Ink Tag / string c is the speaking Character, f is the player_Character aka. Flinta
    private void ChangeSpeechTextToSpeakerPos(string pspeaker)
    {
        switch (pspeaker)
        {
            case "f":
                this.ChangeSpeechTextPos(player, yOffset);
                break;
            case "F":
                this.ChangeSpeechTextPos(player, yOffset);
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


}
