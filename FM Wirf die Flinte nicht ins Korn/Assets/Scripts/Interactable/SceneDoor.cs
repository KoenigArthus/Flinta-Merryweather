using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    public Vector3 spawnpointForNextScene;
    public Quaternion spawnRotation;

    [SerializeField] private string scene;

    public override void ReactToClick(Controller pcon)
    {
        //if this scene is not in the visitedScenes array it gets added
        if (!Array.Exists(pcon.sceneInfo.visitedScenes, element => element == SceneManager.GetActiveScene().name))
        {
            for (int i = 0; i < pcon.sceneInfo.visitedScenes.Length; i++)
            {
                if (pcon.sceneInfo.visitedScenes[i] == null)
                {
                    pcon.sceneInfo.visitedScenes[i] = SceneManager.GetActiveScene().name;
                    break;
                }
            }
        }

        //Audio
        pcon.audioManager.Stop(pcon.currentScene);

        if((pcon.currentScene == "Piratenstadt" && scene == "Taverne")||(pcon.currentScene == "Taverne" && scene == "Piratenstadt"))
        {
            pcon.audioManager.Play("Doorbell");
        }

        ChangeToScene(scene);
    }
  

    public void ChangeToScene(string Scene)
    {
        //saves inventory arrays to SceneInfo-SCO
        controller.sceneInfo.isFull = controller.inventory.isFull;
        controller.sceneInfo.content = controller.inventory.content;
        controller.sceneInfo.spawnpoint = spawnpointForNextScene;
        controller.sceneInfo.spawnpointRotation = spawnRotation;

        controller.playerMovement.Stop();

        SceneManager.LoadScene(Scene);
    } 




}
