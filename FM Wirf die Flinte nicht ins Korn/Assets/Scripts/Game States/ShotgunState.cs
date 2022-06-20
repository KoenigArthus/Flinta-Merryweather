using UnityEngine;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        /*Here can GameObjects of Type Shootable be shot
         *by pressing the middle Mouse Button switches the PLAYER back to the ExploreState
         */

        pcon.lineRenderer.SetPosition(1, new Vector3(pcon.mousePos.x, pcon.mousePos.y, -1));

        if (Input.GetMouseButtonDown(2))
        {
            pcon.lineRenderer.enabled = false;
            pcon.shotgunFilter.enabled = false;
            Cursor.SetCursor(pcon.cursor, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
            return pcon.exploreState;
        }
        else
        {
            return pcon.shotgunState;
        }
    }
}
