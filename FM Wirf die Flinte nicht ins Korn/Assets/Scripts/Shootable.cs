using UnityEngine;

public class Shootable : Interactable
{

    private void Start()
    {
        changesCursorInShotgunState = true;
    }

    public override void ReactToClick(Controller pcon)
    {
            Debug.Log("Get Shot");
    }
}
