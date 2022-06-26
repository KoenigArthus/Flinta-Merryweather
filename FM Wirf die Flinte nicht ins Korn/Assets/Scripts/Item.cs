using UnityEngine;

public class Item : Interactable
{

    [SerializeField] private ScrItem item;

    private string[] sentences;

    private GameObject UIObject;

    //  ScrItem
    private bool isViewable;
    private bool canBePickedUp;
    private bool canBeCombined;


    //initialising the inventory & the ScrItem
    private void Start()
    {
        name = item.name;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
        isViewable = item.isViewable;
        canBePickedUp = item.canBePickedUp;
        canBeCombined = item.canBeCombined;
        UIObject = item.UIObject;
        sentences = item.viewText.Split('|');

    }


    //this function defines, what it should do when it is clicked on 
    public override void ReactToClick(Controller pcon)
    {
        //pressing the left mouse button will pick up the Item if it !canBePickedUp a monologue will appear
        if (Input.GetMouseButtonDown(1) && canBePickedUp)
        {
            this.PickUp();
        }
        else if (Input.GetMouseButtonDown(1) && !canBePickedUp)
        {
            string[] lsentence = new string[] { "ich kann das nicht aufheben." };
            pcon.monologueManager.StartMonologue(lsentence);
        }
        //pressing the right mouse button will view the Item if it is !isViewable be a monologue will appear
        else if (Input.GetMouseButtonDown(0) && isViewable)
        {
            this.View();
        }
        else if (Input.GetMouseButtonDown(0) && !isViewable)
        {
            string[] lsentence = new string[] { name + " ?", "Ich kann es mir nicht anschauen." };
            pcon.monologueManager.StartMonologue(lsentence);
        }
    }


    //Picking up an Item
    private void PickUp()
    {
        //checking if a slot is free and if so then it gets filled with the Object & the player_Character movement disabled
        pcon.playerMovement.Stop();
        for (int i = 0; i < pcon.inventory.slots.Length; i++)
        {
            if (pcon.inventory.isFull[i] == false)
            {
                //fills the content array with the ScrItems
                pcon.inventory.isFull[i] = true;
                item.UIObject.GetComponent<Dragger>().scrItem = item;
                Instantiate(UIObject, pcon.inventory.slots[i].transform, false);
                pcon.inventory.content[i] = item;
                pcon.sceneSave[i] = item.name;
                gameObject.SetActive(false);
                Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot, CursorMode.ForceSoftware);
                break;
            }
            if(i == pcon.inventory.slots.Length - 1)
            {
                Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
            }
        }
    }


    //Viewing an Item
    private void View()
    {
       pcon.monologueManager.StartMonologue(sentences);
    }
}
