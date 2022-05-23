using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettings : MonoBehaviour
{
    void Start()
    {
        //limit the framerate to 120 fps
        Application.targetFrameRate = 120;
    }
    private void Update()
    {
        //for closing the game by pressing P & Return at the same time
        if (Input.GetKeyDown(KeyCode.Return) && Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
    }
}
