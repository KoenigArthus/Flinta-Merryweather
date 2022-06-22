using UnityEngine;

public class Character : Interactable
{
    [SerializeField] private ScrCharacter character;

    private TextAsset ink;


    //Initializing the Character
    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = character.sprite;
    }

    public override void ReactToClick(Controller pcon)
    {
        //checks what .json file has to be used and assigns it to the variable "ink"

        if (!controller.sceneInfo.characters.Contains(character)  && controller.sceneInfo.Regina == false)      
        {
            ink = character.ink;
        }
        else if (!controller.sceneInfo.characters.Contains(character) && controller.sceneInfo.Regina == true)
        {
            ink = character.inkR;
        }
        else if (controller.sceneInfo.characters.Contains(character) && controller.sceneInfo.Regina == false)
        {
            ink = character.ink2;
        }
        else if (controller.sceneInfo.characters.Contains(character) && controller.sceneInfo.Regina == true)
        {
            ink = character.ink2R;
        }


        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan; 
            if (!controller.sceneInfo.characters.Contains(character)) { controller.sceneInfo.characters.Add(character); }
            DialogueManager.GetInstance().EnterDialogueMode(ink, this.gameObject);
        }
    }
}
