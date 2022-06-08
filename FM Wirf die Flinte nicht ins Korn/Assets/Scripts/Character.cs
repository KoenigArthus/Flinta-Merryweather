using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Interactable
{
    //TO DO: Character still needs a Scriptable Object Version

    //this is where the character is given an Ink file
    [SerializeField] private TextAsset ink;

    public override void ReactToClick()
    {
        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<DialogueManager>().EnterDialogueMode(ink, this.gameObject);
        }
    }
}
