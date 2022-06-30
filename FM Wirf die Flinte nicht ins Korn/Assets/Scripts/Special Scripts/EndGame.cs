using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public static bool flintew = true;
    public static bool flintel = false;
    public static bool TS = false;

    public bool coroutineIsPlaying = false;
    string currentText;

    public Animator EndAnimator;
    public Text dialogueText;

    public ScrEnding ending;
    private string[] sentence;
    

    private Queue<string> sentences = new Queue<string>();


    // Start is called before the first frame update
    void Start()
    {
        EndAnimator.Play("End");
        
        if (flintew)
        {
            sentence = ending.Ending1.Split('|');
        }
        else if (flintel)
        {
            sentence = ending.Ending2.Split('|');
        }
        else if (TS)
        {
            sentence = ending.Ending3.Split('|');
        }

        foreach (string lsentence in sentence)
        {
            sentences.Enqueue(lsentence);
        }
        DisplayNextSentence();


    }




    public void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            Application.Quit();
            return;
        }
        if (coroutineIsPlaying)
        {
            StopAllCoroutines();
            dialogueText.text = currentText;
            coroutineIsPlaying = false;

        }
        else
        {
            string lsentence = sentences.Dequeue();
            currentText = lsentence;
            StartCoroutine(TypeSentence(lsentence));

        }

        
    }

    IEnumerator TypeSentence(string lsentence)
    {
        coroutineIsPlaying = true;

        dialogueText.text = "";

        foreach (char letter in lsentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        coroutineIsPlaying = false;

    }


}
