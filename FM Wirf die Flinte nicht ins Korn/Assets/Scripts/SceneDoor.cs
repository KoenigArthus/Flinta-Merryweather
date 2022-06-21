using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    public Vector3 spawnpointForNextScene;

    [SerializeField] private string scene;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }
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

        SceneManager.LoadScene(Scene);
    } 




}
