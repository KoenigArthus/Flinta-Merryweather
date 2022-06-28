using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReactions : MonoBehaviour 
{
   private Controller controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }

    public void Arrivederci()
    {
       Application.Quit();
    }

}
