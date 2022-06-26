using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [HideInInspector] public Controller controller;
    [HideInInspector] public bool changesCursorInShotgunState = false;


    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }

    //when entering and hovering over an Interactable The Cursor color changes
    //when in the shotgun state a crossair is displayed & colored instead
    public virtual void OnMouseEnter()
    {
        if(changesCursorInShotgunState && controller.currentGameState == controller.shotgunState)
        Cursor.SetCursor(controller.crossair1, controller.cursorHotspot,CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && controller.currentGameState == controller.exploreState)
            Cursor.SetCursor(controller.cursor1, controller.cursorHotspot,CursorMode.ForceSoftware);
    }
    public virtual void OnMouseOver()
    {
        if (changesCursorInShotgunState && controller.currentGameState == controller.shotgunState)
            Cursor.SetCursor(controller.crossair1, controller.cursorHotspot,CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && controller.currentGameState == controller.exploreState)
            Cursor.SetCursor(controller.cursor1, controller.cursorHotspot,CursorMode.ForceSoftware);
    }

    public virtual void OnMouseExit()
    {
        if (changesCursorInShotgunState && controller.currentGameState == controller.shotgunState)
            Cursor.SetCursor(controller.crossair0, controller.cursorHotspot,CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && controller.currentGameState == controller.exploreState)
            Cursor.SetCursor(controller.cursor0, controller.cursorHotspot,CursorMode.ForceSoftware);
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick(Controller controller)
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
