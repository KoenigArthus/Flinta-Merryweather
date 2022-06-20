using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Controller controller;

    //when entering and hovering over an Interactable a picture with the Controll Options should pop up
    public virtual void OnMouseEnter()
    {
        Cursor.SetCursor(controller.cursor1, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
    }
    public virtual void OnMouseOver()
    {
        Cursor.SetCursor(controller.cursor1, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
    }

    public virtual void OnMouseExit()
    {
        Cursor.SetCursor(controller.cursor0, new Vector2(0, 0) + new Vector2(8.5f, 8.5f), CursorMode.ForceSoftware);
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick(Controller pcon)
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
