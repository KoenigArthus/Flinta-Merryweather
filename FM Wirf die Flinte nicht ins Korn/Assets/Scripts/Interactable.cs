using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Controller pcon;
    public bool changesCursorInShotgunState = false;


    public void Awake()
    {
        pcon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }



    //when entering and hovering over an Interactable The Cursor color changes
    //when in the shotgun state a crossair is displayed & colored instead
    public virtual void OnMouseEnter()
    {
        if(changesCursorInShotgunState && pcon.currentGameState == pcon.shotgunState)
        Cursor.SetCursor(pcon.crossair1, pcon.cursorHotspot,CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && pcon.currentGameState == pcon.exploreState)
            Cursor.SetCursor(pcon.cursor1, pcon.cursorHotspot,CursorMode.ForceSoftware);
    }
    public virtual void OnMouseOver()
    {
        if (changesCursorInShotgunState && pcon.currentGameState == pcon.shotgunState)
            Cursor.SetCursor(pcon.crossair1, pcon.cursorHotspot,CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && pcon.currentGameState == pcon.exploreState)
            Cursor.SetCursor(pcon.cursor1, pcon.cursorHotspot,CursorMode.ForceSoftware);
    }

    public virtual void OnMouseExit()
    {
        if (changesCursorInShotgunState && pcon.currentGameState == pcon.shotgunState)
            Cursor.SetCursor(pcon.crossair0, pcon.cursorHotspot,CursorMode.ForceSoftware);
        if (!changesCursorInShotgunState && pcon.currentGameState == pcon.exploreState)
            Cursor.SetCursor(pcon.cursor0, pcon.cursorHotspot,CursorMode.ForceSoftware);
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick(Controller pcon)
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
