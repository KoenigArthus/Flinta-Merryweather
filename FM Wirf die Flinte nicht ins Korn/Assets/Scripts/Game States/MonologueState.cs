using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonologueState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1) | Input.GetMouseButtonDown(2)) && !EventSystem.current.IsPointerOverGameObject())
        {
            pcon.monologueManager.DisplayNextSentence();
        }

        return pcon.monologueState;
    }
}
