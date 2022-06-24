using UnityEngine;
using UnityEngine.EventSystems;
public class DraggingState : IGameState
{
    public IGameState RunState(Controller pcon)
    {
        if (!pcon.isDragging)
            return pcon.exploreState;
        else
            return pcon.draggingState;
    }
}