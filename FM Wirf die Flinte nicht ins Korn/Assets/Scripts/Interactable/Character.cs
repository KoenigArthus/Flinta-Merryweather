using UnityEngine;

public class Character : Interactable
{
    public ScrCharacter character;

    private TextAsset ink;
    private string[] sentences;

    //Initializing the Character
    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = character.sprite;
        sentences = character.viewText.Split('|');
        if (!controller.currentSceneWasVisited)
        {
            //character.itemRecieved = false;
        }
    }

    public override void ReactToClick(Controller pcon)
    {
        //checks what .json file has to be used and assigns it to the variable "ink"
        if (ink == character.itemRecieved)
        {
            ink = character.inkI;
        }
        else if (!pcon.sceneInfo.characters.Contains(character) && pcon.sceneInfo.Regina == false)       
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
        else if (pcon.sceneInfo.characters.Contains(character) && character.name == "Regina" && pcon.sceneInfo.Flintendialog)
        {
            ink = character.inkF;
        }

        if (character.name == "Regina" && pcon.sceneInfo.Flintendialog && OneOrBothItemsWereFound("Fischbrot", "Riechsalz") == 0)
        {

        }
        if (character.name == "Regina" && pcon.sceneInfo.Flintendialog && OneOrBothItemsWereFound("Fischbrot", "Riechsalz") == 1)
        {

        }
        if (character.name == "Regina" && pcon.sceneInfo.Flintendialog && OneOrBothItemsWereFound("Fischbrot", "Riechsalz") == 2)
        {

        }


        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            if (!pcon.sceneInfo.characters.Contains(character)) 
            { 
                pcon.sceneInfo.characters.Add(character); 
            }
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

    // das overload React to Click
    public void ReactToClick(Controller pcon,GameObject pgivenItem)
    {
        //increases score for Tavernenschl�gerei-Ending
        if (character.name == "Bertold")
        {
            pcon.sceneInfo.tavernenScore += 2;
        }
        if (character.name == "Dieter")
        {
            pcon.sceneInfo.tavernenScore += 2;
        }
        if (character.name == "PeterLangfinger")
        {
            pcon.sceneInfo.Flintendialog = true;
        }

            ink = character.inkIR;
            character.itemRecieved = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            pcon.dialogueManager.EnterDialogueMode(ink, this.gameObject);
    }
    
    private int OneOrBothItemsWereFound(string psearchitem0, string psearchitem1)
    {
        int lreturnValue = 0;
        for (int i = 0; i < controller.inventory.content.Length; i++)
        {
            if(controller.inventory.content[i] != null && controller.inventory.content[i].name == psearchitem0 || controller.inventory.content[i].name == psearchitem1)
            {
                lreturnValue++;
            }
        }
        return lreturnValue;
    }


}
