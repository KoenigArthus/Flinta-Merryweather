using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        return this;
    }
}
