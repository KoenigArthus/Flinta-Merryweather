using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject OnHoverPreFab;
    public float yOffset, xOffset;

    private GameObject OnHoverPreFabInstance;

    //when entering and hovering over an Interactable a picture with the Controll Options should pop up
    private void OnMouseEnter()
    {
        OnHoverPreFabInstance =  Instantiate(OnHoverPreFab);
    }
    private void OnMouseOver()
    {
        OnHoverPreFabInstance.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + xOffset, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + yOffset, 0);
    }

    private void OnMouseExit()
    {
        Destroy(OnHoverPreFabInstance);
    }

    //this function defines, what the Interactable should do when it is clicked on 
    public virtual void ReactToClick()
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
