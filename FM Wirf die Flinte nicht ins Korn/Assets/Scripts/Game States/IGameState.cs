using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    IGameState RunState(Controller pcon);
}
