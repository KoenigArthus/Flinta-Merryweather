using UnityEngine;

public class Item : Interactable
{

    [SerializeField] private ScrItem item;

    private string[] sentences;

    private GameObject UIObject;
    private Inventory inventory;

    //  ScrItem
    private bool isViewable;
    private bool canBePickedUp;
    private bool canBeCombined;

    private Controller controller;

    private void Awake()
    {
        name = item.name;
    }
    //initialising the inventory & the ScrItem
    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
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
        if (Input.GetMouseButtonDown(0) && canBePickedUp)
        {
            this.PickUp();
        }
        else if (Input.GetMouseButtonDown(0) && !canBePickedUp)
        {
            string[] lsentence = new string[] { "ich kann das nicht aufheben." };
            pcon.monologueManager.StartMonologue(lsentence);
        }
        //pressing the right mouse button will view the Item if it is !isViewable be a monologue will appear
        else if (Input.GetMouseButtonDown(1) && isViewable)
        {
            this.View();
        }
        else if (Input.GetMouseButtonDown(1) && !isViewable)
        {
            string[] lsentence = new string[] { name + " ?", "Ich kann es mir nicht anschauen." };
            pcon.monologueManager.StartMonologue(lsentence);
        }
    }


    //Picking up an Item
    private void PickUp()
    {
        controller.playerMovement.isMoving = false;
        for (int i = 0; i < controller.inventory.slots.Length; i++)
        {
            //checking if a slot is free and if so then it gets filled with the Object & the player_Character movement enabled
            if (controller.inventory.isFull[i] == false)
            {
                controller.inventory.isFull[i] = true;
                //fills the content array with the ScrItems
                Instantiate(UIObject, controller.inventory.slots[i].transform, false);
                controller.inventory.content[i] = item; 
                Destroy(gameObject);
                break;
            }
            if(i == controller.inventory.slots.Length - 1)
            {
                Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
            }
        }
    }


    //Viewing an Item
    private void View()
    {
       controller.monologueManager.StartMonologue(sentences);
    }
}
