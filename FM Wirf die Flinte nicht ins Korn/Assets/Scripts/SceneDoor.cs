using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    public Vector3 spawnpointForNextScene;

    [SerializeField] private string scene;

    private void Start()
    {
        pcon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }
    public override void ReactToClick(Controller pcon)
    {
        ChangeToScene(scene);
    }
  

    public void ChangeToScene(string Scene)
    {
        //saves inventory arrays to SceneInfo-SCO
        pcon.sceneInfo.isFull = pcon.inventory.isFull;
        pcon.sceneInfo.content = pcon.inventory.content;

        pcon.sceneInfo.sceneSave = pcon.sceneSave;

        pcon.sceneInfo.spawnpoint = spawnpointForNextScene;

        SceneManager.LoadScene(Scene);
    } 




}
