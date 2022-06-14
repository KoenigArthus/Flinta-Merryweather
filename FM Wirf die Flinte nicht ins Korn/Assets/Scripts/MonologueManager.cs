using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    public Text dialogueText;

    [SerializeField] private float yOffset = 0.8f;
    

    private Queue<string> sentences;
    private GameObject player;

    // Initializing the Queue & the player_Character
    private void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //this defines what should happen at the start of a monologue
    public void StartMonologue(string[] psentences)
    {
        player.GetComponent<Player_Movement>().isMoving = false;
        gameObject.GetComponent<Controller>().isTalking = true;
        sentences.Clear();

        Vector3 lnewTextPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, 0);
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
       dialogueText.text = lsentence;
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        gameObject.GetComponent<Controller>().isTalking = false;
    }

    // For Debugging only

    //shows the speech text at the player_Character Pos every FixedUpdate
   /* private void FixedUpdate()
    {
        Vector3 lnewTextPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, 0);
        dialogueText.transform.position = Camera.main.WorldToScreenPoint(lnewTextPosition);
    }*/




}
