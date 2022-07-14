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
        //Audio
        controller.audioManager.Play("Close Book");

        Application.Quit();
    }

    public void ShowControlls()
    {
        //Audio
        controller.audioManager.Play("Go Book");

        controller.pauseMenue.SetActive(false);
        controller.controllsMenue.SetActive(true);
    }

    public void BackToPauseMenue()
    {
        //Audio
        controller.audioManager.Play("Write Book");

        controller.controllsMenue.SetActive(false);
        controller.pauseMenue.SetActive(true);
    }
}
