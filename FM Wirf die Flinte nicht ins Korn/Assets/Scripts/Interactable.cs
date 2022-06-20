using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Controller controller;
    public bool changesCursorInShotgunState = false;


    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }



    //when entering and hovering over an Interactable a picture with the Controll Options should pop up
    public virtual void OnMouseEnter()
    {
        if(changesCursorInShotgunState && controller.currentGameState == controller.shotgunState)
        Cursor.SetCursor(controller.crossair1, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && controller.currentGameState != controller.shotgunState)
            Cursor.SetCursor(controller.cursor1, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
    }
    public virtual void OnMouseOver()
    {
        if (changesCursorInShotgunState && controller.currentGameState == controller.shotgunState)
            Cursor.SetCursor(controller.crossair1, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && controller.currentGameState != controller.shotgunState)
            Cursor.SetCursor(controller.cursor1, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
    }

    public virtual void OnMouseExit()
    {
        if (changesCursorInShotgunState && controller.currentGameState == controller.shotgunState)
            Cursor.SetCursor(controller.crossair0, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && controller.currentGameState != controller.shotgunState)
            Cursor.SetCursor(controller.cursor0, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick(Controller pcon)
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
