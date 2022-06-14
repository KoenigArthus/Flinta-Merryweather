using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        return this;
    }
}
