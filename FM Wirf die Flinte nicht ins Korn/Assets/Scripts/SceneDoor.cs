using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    public string scene;
    public override void ReactToClick()
    {
        ChangeToScene(scene);
    }


    public void ChangeToScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }




}
