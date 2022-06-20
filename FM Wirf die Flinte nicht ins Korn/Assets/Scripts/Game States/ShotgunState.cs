using UnityEngine;
using UnityEngine.EventSystems;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        pcon.lineRenderer.SetPosition(1, new Vector3(pcon.mousePos.x, pcon.mousePos.y, -1));


        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            pcon.hit = Physics2D.Raycast(pcon.mousePos, Vector2.zero);
            if (pcon.hit.collider != null && pcon.hit.collider.gameObject.CompareTag("Shootable"))
            {
                pcon.hit.collider.gameObject.SendMessage("ReactToClick", pcon);
                pcon.lineRenderer.enabled = false;
                pcon.shotgunFilter.enabled = false;
                Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot,CursorMode.ForceSoftware);
                return pcon.exploreState;
            }
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
