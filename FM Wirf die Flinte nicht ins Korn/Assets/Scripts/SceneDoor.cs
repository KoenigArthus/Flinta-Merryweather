using UnityEngine.SceneManagement;

public class SceneDoor : Interactable
{
    public string scene;
    public override void ReactToClick(Controller pcon)
    {
        ChangeToScene(scene);
    }


    public void ChangeToScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }




}
