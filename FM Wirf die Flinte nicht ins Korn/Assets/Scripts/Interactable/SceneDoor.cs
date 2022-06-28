using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    public Vector3 spawnpointForNextScene;
    public Quaternion spawnRotation;

    [SerializeField] private string scene;

    public override void ReactToClick(Controller pcon)
    {
        ChangeToScene(scene);
    }
  

    public void ChangeToScene(string Scene)
    {
        //saves inventory arrays to SceneInfo-SCO
        controller.sceneInfo.isFull = controller.inventory.isFull;
        controller.sceneInfo.content = controller.inventory.content;
        controller.sceneInfo.sceneSave = controller.sceneSave;
        controller.sceneInfo.spawnpoint = spawnpointForNextScene;
        controller.sceneInfo.spawnpointRotation = spawnRotation;

        SceneManager.LoadScene(Scene);
    } 




}
