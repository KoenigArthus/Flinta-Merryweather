using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReactions : MonoBehaviour 
{
   private Controller controller;

    private void Start()
    {
        controller = this.gameObject.GetComponent<Controller>();
    }

    public void Arrivederci()
    {
       Application.Quit();
    }

    public void ShowControlls()
    {
        controller.pauseMenue.SetActive(false);
        controller.controllsMenue.SetActive(true);
    }

    public void BackToPauseMenue()
    {
        controller.controllsMenue.SetActive(false);
        controller.pauseMenue.SetActive(true);
    }

}
