using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    //changed to TMP instead of Legacy Text
    public Text dialogueText;

    [SerializeField] private float yOffset = 0.8f;

    private Color flintaColor;
    private Controller controller;
    private Queue<string> sentences = new Queue<string>();

    // Initializing the Queue & the player_Character
    private void Start()
    {
        ColorUtility.TryParseHtmlString("#EC8085", out flintaColor);
        controller = gameObject.GetComponent<Controller>();
    }

    //this defines what should happen at the start of a monologue
    public void StartMonologue(string[] psentences)
    {

        foreach (SpriteRenderer child in controller.childRenderer)
        {
            child.gameObject.GetComponentInChildren<SpriteRenderer>().color = controller.filterColor;
        }

        controller.playerMovement.Stop();
        controller.talkingState.monologueIsPlaying = true;
        sentences.Clear();

        Vector3 lnewTextPosition = new Vector3(controller.player.transform.position.x, controller.player.transform.position.y + yOffset, 0);
        dialogueText.transform.position = Camera.main.WorldToScreenPoint(lnewTextPosition);

        foreach (string lsentence in psentences)
        {
            sentences.Enqueue(lsentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
       string lsentence = sentences.Dequeue();
       dialogueText.color = flintaColor;
       dialogueText.text = lsentence;
    }

    public void EndDialogue()
    {
        foreach (SpriteRenderer child in controller.childRenderer)
        {
            child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        dialogueText.text = "";
        controller.talkingState.monologueIsPlaying = false;
    }

    // For Debugging only

    //shows the speech text at the player_Character Pos every FixedUpdate
   /* private void FixedUpdate()
    {
        Vector3 lnewTextPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, 0);
        dialogueText.transform.position = Camera.main.WorldToScreenPoint(lnewTextPosition);
    }*/




}
