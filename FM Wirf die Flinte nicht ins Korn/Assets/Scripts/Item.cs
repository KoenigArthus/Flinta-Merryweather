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

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public override void ReactToClick()
    {
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
            else
            {
                //Here Should pop up a text above Flinta, that says something like "I cannot pick up" + Item.Name 
                Debug.Log("Inventory full");
            }
        }
    }
}