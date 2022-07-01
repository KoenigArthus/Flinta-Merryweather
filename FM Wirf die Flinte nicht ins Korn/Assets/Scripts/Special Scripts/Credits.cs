using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator animator;





    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }

}
