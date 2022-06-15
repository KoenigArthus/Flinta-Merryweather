using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    [SerializeField] private string scene;
    private Controller controller;

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

        SceneManager.LoadScene(Scene);
    } 




}
