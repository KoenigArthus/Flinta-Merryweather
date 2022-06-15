using UnityEngine;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        /*Here can GameObjects of Type Shootable be shot
         *by pressing the middle Mouse Button switches the PLAYER back to the ExploreState
         */

        if (Input.GetMouseButtonDown(2))
        {
            pcon.shotgunFilter.SetActive(false);
            return pcon.exploreState;
        }
        else
        {
            return pcon.shotgunState;
        }
    }
}
