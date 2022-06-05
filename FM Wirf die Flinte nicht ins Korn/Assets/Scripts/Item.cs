using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public ScrItem item;

    private string[] sentences;
    private bool isViewable;
    private bool canBePickedUp;
    private bool canBeCombined;

    private GameObject UIObject;

    private Inventory inventory;

    //initialising the inventory & the ScrItem
    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
        isViewable = item.isViewable;
        canBePickedUp = item.canBePickedUp;
        canBeCombined = item.canBeCombined;
        UIObject = item.UIObject;
        sentences = item.viewText.Split('|');
    }

    //this function defines, what it should do when it is clicked on 
    public override void ReactToClick()
    {
        //pressing the left mouse button will pick up the Item
        if (Input.GetMouseButtonDown(0) && canBePickedUp)
        {
            this.PickUp();
        }
        else if (Input.GetMouseButtonDown(0) && !canBePickedUp)
        {
            // Flinta schould say something like "Ich kann" + this.name "nicht aufheben."
        }
        else if (Input.GetMouseButtonDown(1) && isViewable)
        {
            Debug.Log("asdasdasdasda");
            this.View();
        }
        else if (Input.GetMouseButtonDown(1) && !isViewable)
        {
            // Flinta schould say something like "Ich kann" + this.name "nicht angucken."
        }
    }

    //Picking up an Item
    private void PickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            //checking if a slot is free and if so then it gets filled with the Object & the player_Character movement enabled
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
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
        FindObjectOfType<SmalDialogueManager>().StartDialogue(name, sentences);
    }
}