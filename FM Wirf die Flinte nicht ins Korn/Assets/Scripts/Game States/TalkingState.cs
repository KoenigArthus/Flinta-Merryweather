using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TalkingState : IGameState
{
    public bool dialogueIsPlaying = false;
    public bool monologueIsPlaying = false;


    public IGameState RunState(Controller pcon)
    {
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1) | Input.GetMouseButtonDown(2)) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (dialogueIsPlaying)
            {
                pcon.dialogueManager.ContinueStory();
                if (!dialogueIsPlaying)
                    return pcon.exploreState;
            }
                
            if (monologueIsPlaying)
            {
                pcon.monologueManager.DisplayNextSentence();
                if (!monologueIsPlaying)
                    return pcon.exploreState;
            }
               
        }

        return pcon.talkingState;
    }
}
