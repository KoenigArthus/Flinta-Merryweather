using UnityEngine;
using UnityEngine.EventSystems;

public class TalkingState : IGameState
{
    //these variables determine if flinta is in a monologue or dialogue they should stay in an xor relation to each other
    public bool dialogueIsPlaying = false;
    public bool monologueIsPlaying = false;

    /*clicking with any mouse button anywhere except on an UIElement will result in continuing the dialogue/monologue 
    * when the dialogue ends switches the game back to the ExploreState automatically
    * this is managed by the dialogueIsPLaying & monologueIsPLaying variables that are accessed and changed by the respective manager class
    */
    public IGameState RunState(Controller pcon)
    {
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1) | Input.GetMouseButtonDown(2)) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (dialogueIsPlaying)
            {
                pcon.dialogueManager.ContinueStory();
                if (!dialogueIsPlaying)
                {
                    Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot, CursorMode.ForceSoftware);
                    return pcon.exploreState;
                }
            }
                
            if (monologueIsPlaying)
            {
                pcon.monologueManager.DisplayNextSentence();
                if (!monologueIsPlaying)
                {
                    Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot, CursorMode.ForceSoftware);
                    return pcon.exploreState;
                }
            }
               
        }

        return pcon.talkingState;
    }
}
