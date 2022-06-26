using System.Collections;
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

            if (child.gameObject.GetComponentInChildren<SpriteRenderer>().color == Color.cyan)
            {
                child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

            }
            else
            {
                StartCoroutine(FadeIn(child));
            }

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
            StartCoroutine(FadeOut(child));
        }

        dialogueText.text = "";
        controller.talkingState.monologueIsPlaying = false;
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

    // For Debugging only

    //shows the speech text at the player_Character Pos every FixedUpdate
    /* private void FixedUpdate()
     {
         Vector3 lnewTextPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, 0);
         dialogueText.transform.position = Camera.main.WorldToScreenPoint(lnewTextPosition);
     }*/




}
