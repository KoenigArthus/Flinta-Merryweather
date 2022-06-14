using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //when entering and hovering over an Interactable a picture with the Controll Options should pop up
    private void OnMouseEnter()
    {
        
    }
    private void OnMouseOver()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick(Controller pcon)
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
