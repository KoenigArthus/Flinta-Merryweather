using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{

    [SerializeField] private ScrItem item;
    [SerializeField] private SceneInfo sceneInfo;

    private string[] sentences;

    private GameObject UIObject;
    private Inventory inventory;

    //  ScrItem
    private bool isViewable;
    private bool canBePickedUp;
    private bool canBeCombined;

   

    //initialising the inventory & the ScrItem
    private void Awake()
    {

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); //controller?



        this.gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;

        isViewable = item.isViewable;
        canBePickedUp = item.canBePickedUp;
        canBeCombined = item.canBeCombined;

        UIObject = item.UIObject;
        sentences = item.viewText.Split('|');

        //sets nventory arrays to SceneInfo Arrays
        inventory.isFull = sceneInfo.isFull;
        inventory.content = sceneInfo.content;


    }

    //resets sceneInfo arrays + instantiates items from inventory back into UI-Element
    private void Start()
    {
         sceneInfo.isFull = new bool[13];
         sceneInfo.content = new ScrItem[13];

        for(int i = 0; i < inventory.isFull.Length; i++)
        {

            if (inventory.isFull[i] == true)
            {
                Instantiate(inventory.content[i].UIObject, inventory.slots[i].transform, false);
            }

        }

    }


    //this function defines, what it should do when it is clicked on 
    public override void ReactToClick()
    {
        //pressing the left mouse button will pick up the Item if it !canBePickedUp a monologue will appear
        if (Input.GetMouseButtonDown(0) && canBePickedUp)
        {
            this.PickUp();
        }
        else if (Input.GetMouseButtonDown(0) && !canBePickedUp)
        {
            string[] lsentence = new string[] { "Ich kann das nicht aufheben." };
            FindObjectOfType<MonologueManager>().StartDialogue(lsentence);
        }
        //pressing the right mouse button will view the Item if it is !isViewable be a monologue will appear
        else if (Input.GetMouseButtonDown(1) && isViewable)
        {
            this.View();
        }
        else if (Input.GetMouseButtonDown(1) && !isViewable)
        {
            string[] lsentence = new string[]{name + " ?", "Ich kann es mir nicht anschauen."};
            FindObjectOfType<MonologueManager>().StartDialogue(lsentence);
        }
    }


    //Picking up an Item
    private void PickUp()
    {
        inventory.gameObject.GetComponent<Player_Movement>().isMoving = false; //controller?


        for (int i = 0; i < inventory.slots.Length; i++)
        {
            //checking if a slot is free and if so then it gets filled with the Object & the player_Character movement enabled
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;

                //fills the content array with the ScrItems
                inventory.content[i] = item; 

                Instantiate(UIObject, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
            if(i == inventory.slots.Length - 1)
            {
                Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
            }
        }
    }


    //Viewing an Item
    private void View()
    {
        FindObjectOfType<MonologueManager>().StartDialogue(sentences);
    }
}