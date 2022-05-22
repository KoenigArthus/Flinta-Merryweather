using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject OnHoverPreFab;
    public float yOffset, xOffset;

    private void OnMouseEnter()
    {
        OnHoverPreFab.SetActive(true);
    }
    private void OnMouseOver()
    {
        OnHoverPreFab.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + xOffset, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + yOffset, 0);
    }

    private void OnMouseExit()
    {
        OnHoverPreFab.SetActive(false);
    }

    public virtual void ReactToClick()
    {
        Debug.LogError("no override defined pls create a + ReactToClick: override void");
    }

}
