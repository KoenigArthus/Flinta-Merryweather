using UnityEngine;
using UnityEngine.EventSystems;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        // reseting shooting animation
        if(pcon.animator.GetBool("isShooting"))
        pcon.animator.SetBool("isShooting", false);

        //activating line renderer & the aiming animation when first clicked
        if (Input.GetMouseButtonDown(1))
        {
            pcon.lineRenderer.enabled = true;
            pcon.animator.SetBool("isAiming", true);
        }

        //when holding right mouse button
        if (Input.GetKey(KeyCode.Mouse1))    
        {
            //line renderer pos gets updated
            pcon.lineRenderer.SetPosition(1, new Vector3(pcon.mousePos.x, pcon.mousePos.y, -3));
            if (pcon.mousePos.x < pcon.transform.position.x)
            {
                pcon.player.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            }
            else
            {
                pcon.player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            //shooting when also clicking the left mouse button 
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                pcon.animator.SetBool("isShooting", true);
                pcon.hit = Physics2D.Raycast(pcon.mousePos, Vector2.zero);
                if (pcon.hit.collider != null && pcon.hit.collider.gameObject.CompareTag("Shootable"))
                {
                    pcon.animator.SetBool("didHitSomething", true);
                    pcon.animator.SetBool("isAiming", false);
                    pcon.hit.collider.gameObject.SendMessage("ReactToClick", pcon);
                    pcon.lineRenderer.enabled = false;
                    pcon.shotgunFilter.enabled = false;
                    Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot, CursorMode.ForceSoftware);
                    return pcon.exploreState;
                }
                else
                {
                    pcon.animator.SetBool("didHitSomething", false);
                    return pcon.shotgunState;
                }

            }
        }

        //resetting line renderer and aiming animation when letting go left mouse button
        if (Input.GetMouseButtonUp(1))
        {
            pcon.lineRenderer.enabled = false;
            pcon.animator.SetBool("isAiming", false);
        }

        if (Input.GetMouseButtonDown(2))
        {
            pcon.lineRenderer.enabled = false;
            pcon.shotgunFilter.enabled = false;
            Cursor.SetCursor(pcon.cursor0, new Vector2(0, 0) + new Vector2(8.5f, 8.5f),CursorMode.ForceSoftware);
            return pcon.exploreState;
        }
        else
        {
            return pcon.shotgunState;
        }
    }
}
