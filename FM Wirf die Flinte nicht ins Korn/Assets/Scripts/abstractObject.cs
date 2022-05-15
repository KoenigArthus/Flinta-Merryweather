using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstractObject : MonoBehaviour
{
    // variables for actions with Objects - the ingame ones obviously
    public bool isViewable;
    public bool isPlacable;
    public bool canBePickedUp;
    public bool canBeCombined;

    public ScriptableThing thing;

    private GameObject UIObject;
    // to talk to the Player_Movement & Inventory Script of the player_Chacarter 
    private Player_Movement player_movement;
    private Inventory inventory;

    private void Awake()
    {
        //referencing the Player_Movement & Inventory Script of the PLayer_Character
        player_movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void Start()
    {
        isViewable = thing.isViewable;
        isPlacable = thing.isPlacable;
        canBePickedUp = thing.canBePickedUp;
        canBeCombined = thing.canBeCombined;
        UIObject = thing.UIObject;
    }


    private void OnMouseEnter()
    {
        //disabling the player_Character movement
        player_movement.movementIsEnabled = false;
    }

    private void OnMouseExit()
    {
        //endabling the player_Character movement
        player_movement.movementIsEnabled = true;
    }

    private void OnMouseDown()
    {
        if (canBePickedUp && Input.GetMouseButtonDown(0))
        {
            this.pickUp();
        }
    }


    private void pickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            //checking if a slot is free and if so then it gets filled with the Object & the player_Character movement enabled
            if(inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(UIObject, inventory.slots[i].transform, false);
                Destroy(gameObject);
                player_movement.movementIsEnabled = true;
                break;
            }
        }
    }
}
