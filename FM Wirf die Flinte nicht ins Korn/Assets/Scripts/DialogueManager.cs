using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public bool choicesEnabled = false;

    [Header("Dialogue UI")]
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject uiInventory;
    [SerializeField] private float yOffset = 0.8f;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject choicespanel;
    [SerializeField] private Text[] choicesText;
  
    private GameObject player;
    private Controller controller;
    private GameObject speakingCharacter;
    private Animator buttonPopUp;
    private Story currentStory;
    private Color flintaColor;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string LAYOUT_TAG = "layout";
    private const string STATE_TAG = "state";


    //checks if only one DialogueManager is in the scene + deactivates the UI-Assets that are only supposed to be active in DialogueMode
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        choicespanel.SetActive(false);

        ColorUtility.TryParseHtmlString("#EC8085", out flintaColor);
        
    }


    //Returns the instance (used to access the singleton in other scripts without using the Unity_Inspector
    public static DialogueManager GetInstance()
    {
        return instance;
    }


    //accesess all choice-buttons + accesses the choice-button animator
    private void Start()
    {
        choicesText = new Text[choices.Length];

        buttonPopUp = choicespanel.GetComponent<Animator>();

        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<Text>();
            index++;
        }

        controller = gameObject.GetComponent<Controller>();

        
    }


    public void EnterDialogueMode(TextAsset inkJSON, GameObject pcharacter)
    {
   
        foreach (SpriteRenderer child in controller.childRenderer)
        {
  
            if (child.gameObject.GetComponentInChildren<SpriteRenderer>().color == Color.cyan)
            {
                child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
   
            }
            else
            {
                StartCoroutine(FadeIn(child));
            }
            
        }

        


            controller.talkingState.dialogueIsPlaying = true;
    //Starts the DialogueMode (sets UI-elements active, accesses the current .json, calls ContinueStory())
            choicespanel.SetActive(true);
            uiInventory.SetActive(false);

            speakingCharacter = pcharacter;
            controller.playerMovement.Stop();
            currentStory = new Story(inkJSON.text);
            ContinueStory();
    }

   


    //Ends the DialogueMode (deactivates UI-elements + sets dialogueIsPlaying bool to false)
    private void ExitDialogueMode()
    {
        foreach (SpriteRenderer child in controller.childRenderer)
        {
            StartCoroutine(FadeOut(child));
        }
       

        uiInventory.SetActive(true);
        controller.talkingState.dialogueIsPlaying = false;
        dialogueText.text = "";

    }


    //checks if the story (in .json File) can continue 
    public void ContinueStory()
    {
        //checks and reacts to them if there are choices or tags + sets animation to default state + continues the current story
        if (currentStory.canContinue)
        {

            dialogueText.text = currentStory.Continue();
            buttonPopUp.Play("ChoiceButtonDefault");
            DisplayChoices();
            TagHandler(currentStory.currentTags);

        }
        else if (!currentStory.canContinue && !choicesEnabled)
        {
            ExitDialogueMode();
        }
    }


    //this is called through the choice-buttons and continues the story accordingly + stops the choice-button animation
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        choicesEnabled = false;
        buttonPopUp.SetBool("choicesEnabled", false);
    }


    //checks what tags are written in the .json file and reacts accordingly
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
                        controller.sceneInfo.Regina = true;
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    //creates a list with all the current choices. If there are choices in the list, it will start up the choice-button animations + activate the buttons (+ deactivates all unused buttons)
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > 0)
        {
            choicesEnabled = true;
            buttonPopUp.SetBool("choicesEnabled", true);
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
                dialogueText.color = flintaColor;
                break;
            case "F":
                this.ChangeSpeechTextPos(controller.player, yOffset);
                break;
            case "c":
                this.ChangeSpeechTextPos(speakingCharacter, yOffset);
                dialogueText.color = Color.white;
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

    //Fades the talkingFilter in/out
    IEnumerator FadeIn(SpriteRenderer child)
    {
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.1f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.2f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.3f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.4f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.5f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.6f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.7f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.8f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 0.9f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, controller.filterColor, 1f);
        yield return null;
    }
    IEnumerator FadeOut(SpriteRenderer child)
    {
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.1f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.2f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.3f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.4f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.5f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.6f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.7f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.8f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 0.9f);
        yield return new WaitForSeconds(0.05f);
        child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(controller.filterColor, Color.white, 1f);
        yield return null;
    }

}
