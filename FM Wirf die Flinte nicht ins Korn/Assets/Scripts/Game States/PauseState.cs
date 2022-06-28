using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IGameState
{
    private bool started;

    public IGameState RunState(Controller pcon)
    {
        if (!started)
        this.StartState(pcon);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            started = false;
            pcon.pauseMenue.SetActive(false);
            if (pcon.oldState == pcon.talkingState)
            {
                Cursor.SetCursor(pcon.cursor1, pcon.cursorHotspot, CursorMode.ForceSoftware);
            }
            return pcon.oldState;
        }

        //stay
        return pcon.pauseState;
    }

    private void StartState(Controller pcon)
    {
        Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot, CursorMode.ForceSoftware);
        pcon.playerMovement.Stop();
        pcon.pauseMenue.SetActive(true);
        started = true;
    }


}
