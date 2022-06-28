using UnityEngine;

public class Character : Interactable
{
    [SerializeField] private ScrCharacter character;

    private TextAsset ink;
    private string[] sentences;


    //Initializing the Character
    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = character.sprite;
        sentences = character.viewText.Split('|');
    }

    public override void ReactToClick(Controller pcon)
    {

        //checks what .json file has to be used and assigns it to the variable "ink"
        if (!pcon.sceneInfo.characters.Contains(character)  && pcon.sceneInfo.Regina == false)      
        {
            ink = character.ink;
        }
        else if (!pcon.sceneInfo.characters.Contains(character) && pcon.sceneInfo.Regina == true)
        {
            ink = character.inkR;
        }
        else if (pcon.sceneInfo.characters.Contains(character) && pcon.sceneInfo.Regina == false)
        {
            ink = character.ink2;
        }
        else if (pcon.sceneInfo.characters.Contains(character) && pcon.sceneInfo.Regina == true)
        {
            ink = character.ink2R;
        } 
        else if (character.itemRecieved == true)
        {
            ink = character.inkI;
        }


        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            if (!pcon.sceneInfo.characters.Contains(character)) { pcon.sceneInfo.characters.Add(character); }
            DialogueManager.GetInstance().EnterDialogueMode(ink, this.gameObject);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            this.View();
        }
    }
    private void View()
    {
        controller.monologueManager.StartMonologue(sentences);
    }

    public void ReactToClick(Controller pcon,GameObject pgivenItem)
    {
        ink = character.inkIR;
        character.itemRecieved = true;
        pcon.dialogueManager.EnterDialogueMode(ink, this.gameObject);
    }

}
