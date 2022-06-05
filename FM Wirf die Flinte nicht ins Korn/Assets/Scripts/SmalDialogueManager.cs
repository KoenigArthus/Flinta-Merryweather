using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmalDialogueManager : MonoBehaviour
{

    public Text nametext;
    public Text dialoguetext;
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string pname, string[] psentences)
    {
        nametext.text = pname;

        sentences.Clear();

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
       dialoguetext.text = lsentence;
    }

    public void EndDialogue()
    {
        Debug.Log("end");
    }
}
