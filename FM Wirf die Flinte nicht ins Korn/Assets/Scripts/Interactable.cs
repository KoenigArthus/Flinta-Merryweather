using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Controller controller;

    //when entering and hovering over an Interactable a picture with the Controll Options should pop up
    public virtual void OnMouseEnter()
    {
        
    }
    public virtual void OnMouseOver()
    {
        
    }

    public virtual void OnMouseExit()
    {
        
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick(Controller pcon)
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
