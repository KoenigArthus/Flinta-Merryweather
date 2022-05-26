using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public bool isViewable;
    public bool isPlacable;
    public bool canBePickedUp;
    public bool canBeCombined;

    public GameObject UIObject;

    private Inventory inventory;

    //initialising the inventory
    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }


    //this function defines, what it should do when it is clicked on 
    public override void ReactToClick()
    {
        //pressing the left mouse button will pick up the Item
        if (Input.GetMouseButtonDown(0) && canBePickedUp)
        {
            this.pickUp();
        }
    }
    private void pickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            //checking if a slot is free and if so then it gets filled with the Object & the player_Character movement enabled
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(UIObject, inventory.slots[i].transform, false);
                OnHoverPreFab.SetActive(false);
                Destroy(gameObject);
                break;
            }
            if(i == inventory.slots.Length - 1)
            {
                Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
            }
        }
    }
}