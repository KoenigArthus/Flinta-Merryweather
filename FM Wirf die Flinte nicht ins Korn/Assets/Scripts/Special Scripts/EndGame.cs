using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public static bool flintew = true;
    public static bool flintel = false;
    public static bool TS = false;

    public Animator EndAnimator;
    public Text dialogueText;
    public GameObject continueButton;
    public GameObject EndButton;

    public ScrEnding ending;
    private string[] sentence;
    

    private Queue<string> sentences = new Queue<string>();


    // Start is called before the first frame update
    void Start()
    {
        EndAnimator.Play("End");
        
        if (flintew && !flintel && !TS)
        {
            sentence = ending.Ending1.Split('|');
        }
        else if (flintel && !TS)
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

    public void ContinueButton()
    {
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            continueButton.SetActive(false);
            EndButton.SetActive(true);
            return;
        }
        string lsentence = sentences.Dequeue();

        dialogueText.text = lsentence;
    }

    public void Enditall()
    {
        Application.Quit();
    }

}
