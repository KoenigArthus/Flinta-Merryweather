using UnityEngine;
using UnityEngine.EventSystems;

public class ExploreState : IGameState
{
    //During the Explore State The PLAYER can walk around and interact with Character and Items
    public IGameState RunState(Controller pcon)
    {
        if (pcon.animator.GetBool("isShooting"))
            pcon.animator.SetBool("isShooting", false);
        /* when clicking left or riht mouse button on an Interactactable it will call its ReactToClick Funktion
         * when the player ends up talking to a Character or through an Item descripion -> the State will be updated to talkingState
         * when the player is not in reach of the Interactable or clicks anywhere else other than on an UIElement ...
         * ... then will the Player_Character walk to the MousePos
         */
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

        // pressing the middle Mouse Button will result in switching to the shotgunState
        if (Input.GetMouseButtonDown(2))
        {
            pcon.playerMovement.Stop();
            pcon.shotgunFilter.enabled = true;
            pcon.lineRenderer.SetPosition(0, new Vector3 (pcon.player.transform.position.x, pcon.player.transform.position.y, -3));
            Cursor.SetCursor(pcon.crossair0, pcon.cursorHotspot,CursorMode.ForceSoftware);
            return pcon.shotgunState;
        }
        else
        {
            return pcon.exploreState;
        }
    }
}
