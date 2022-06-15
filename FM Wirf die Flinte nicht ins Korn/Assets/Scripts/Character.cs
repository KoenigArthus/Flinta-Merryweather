using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Interactable
{
    [SerializeField] private ScrCharacter character;
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private Inventory inventory;

    private TextAsset ink;

    private bool Regina;


    //Initializing the Character
    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = character.sprite;
    }

    public override void ReactToClick()
    {
        //checks what .json file has to be used and assigns it to the variable "ink"
        Regina = sceneInfo.Regina;

        if (!sceneInfo.characters.Contains(character)  && sceneInfo.Regina == false)      
        {
            ink = character.ink;
        }
        else if (!sceneInfo.characters.Contains(character) && sceneInfo.Regina == true)
        {
            ink = character.inkR;
        }
        else if (sceneInfo.characters.Contains(character) && sceneInfo.Regina == false)
        {
            ink = character.ink2;
        }
        else if (sceneInfo.characters.Contains(character) && sceneInfo.Regina == true)
        {
            ink = character.ink2R;
        }


        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            if (!sceneInfo.characters.Contains(character)) { sceneInfo.characters.Add(character); }
            DialogueManager.GetInstance().EnterDialogueMode(ink, this.gameObject);
        }
    }
}
