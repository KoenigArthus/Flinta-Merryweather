using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private Inventory inventory;
    [SerializeField] private string scene;

    public override void ReactToClick()
    {
        ChangeToScene(scene);
    }
  

    public void ChangeToScene(string Scene)
    {
        //saves inventory arrays to SceneInfo-SCO
        sceneInfo.isFull = inventory.isFull;
        sceneInfo.content = inventory.content;

        SceneManager.LoadScene(Scene);
    } 




}
