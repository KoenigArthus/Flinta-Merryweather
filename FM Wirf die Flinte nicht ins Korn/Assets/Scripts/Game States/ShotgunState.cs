using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        if (Input.GetMouseButtonDown(2))
        {
            return pcon.exploreState;
        }
        else
        {
            return pcon.shotgunState;
        }
    }
}
