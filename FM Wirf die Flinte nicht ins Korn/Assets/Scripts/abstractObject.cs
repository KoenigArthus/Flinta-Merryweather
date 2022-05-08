using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstractObject : MonoBehaviour
{
    // variables for actions with Objects - the ingame ones obviously
    public bool isViewable;
    public bool canBePickedUp;
    public bool canBeCombined;
    public bool isPlacable;

    // to talk to the Player_Movement Script of the player_Chacarter / later disabling the movement
    private Player_Movement player_movement;

    private void Awake()
    {
        //referencing the Player_Movement Script of the PLayer_Character
        player_movement = GameObject.FindWithTag("Player").GetComponent<Player_Movement>();
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
}
