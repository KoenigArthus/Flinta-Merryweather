using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettings : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 120;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
    }
}
