using UnityEngine;

public class Shootable : Interactable
{

    private void Start()
    {
        changesCursorInShotgunState = true;
    }

    public override void ReactToClick(Controller pcon)
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Get Shot");
        }
    }
}
