using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Interactable
{
    public ScrCharacter character;

     private TextAsset ink;

    //Initializing the Character
    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = character.sprite;
        ink = character.ink;
    }
    public override void ReactToClick()
    {
        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<DialogueManager>().EnterDialogueMode(ink, this.gameObject);
        }
    }
}
