using UnityEngine;
using UnityEngine.EventSystems;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        if(pcon.animator.GetBool("isShooting"))
        pcon.animator.SetBool("isShooting", false);

        if (Input.GetMouseButtonDown(1))
        {
            pcon.lineRenderer.enabled = true;
            pcon.animator.SetBool("isAiming", true);
        }

        if (Input.GetKey(KeyCode.Mouse1))    
        {
            pcon.lineRenderer.SetPosition(1, new Vector3(pcon.mousePos.x, pcon.mousePos.y, -1));
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                pcon.animator.SetBool("isShooting", true);
                pcon.hit = Physics2D.Raycast(pcon.mousePos, Vector2.zero);
                if (pcon.hit.collider != null && pcon.hit.collider.gameObject.CompareTag("Shootable"))
                {
                    pcon.animator.SetBool("didHitSomething", true);
                    pcon.animator.SetBool("isAiming", false);
                    //pcon.animator.SetBool("isShooting", false);
                    pcon.hit.collider.gameObject.SendMessage("ReactToClick", pcon);
                    pcon.lineRenderer.enabled = false;
                    pcon.shotgunFilter.enabled = false;
                    Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot, CursorMode.ForceSoftware);
                    return pcon.exploreState;
                }
                else
                {
                    pcon.animator.SetBool("didHitSomething", false);
                    //pcon.animator.SetBool("isShooting", false);
                    Debug.Log("nothing hit");
                    return pcon.shotgunState;
                }

            }
        }

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
