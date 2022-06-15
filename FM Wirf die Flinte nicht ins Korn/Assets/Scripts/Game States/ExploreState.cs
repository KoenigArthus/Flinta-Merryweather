using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExploreState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1)) && !EventSystem.current.IsPointerOverGameObject())
        {
            pcon.hit = Physics2D.Raycast(pcon.mousePos, Vector2.zero);
            if (pcon.hit.collider != null && pcon.IsInReach() && pcon.hit.collider.gameObject.CompareTag("Interactable"))
            {
                pcon.hit.collider.gameObject.SendMessage("ReactToClick", pcon);
                if(pcon.talkingState.monologueIsPlaying | pcon.talkingState.dialogueIsPlaying)
                {
                   return pcon.talkingState;
                }
            }
            // if none of the above is true then the player moves to the mousePos
            else
            {
                pcon.player.GetComponent<PlayerMovement>().MoveTo(pcon.mousePos);
            }


        }


        if (Input.GetMouseButtonDown(2))
        {
            return pcon.shotgunState;
        }
        else
        {
            return pcon.exploreState;
        }
    }






}
