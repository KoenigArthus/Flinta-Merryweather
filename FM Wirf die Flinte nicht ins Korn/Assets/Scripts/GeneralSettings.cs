using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettings : MonoBehaviour
{
    // Start is called before the first frame update
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
