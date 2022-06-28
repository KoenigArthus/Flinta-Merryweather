using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public SceneInfo sceneInfo;

    public void StartTheGame()
    {
        ///Start Button Stuff:

        //resets sceneInfo
        sceneInfo.isFull = new bool[13];
        sceneInfo.content = new ScrItem[13];
        sceneInfo.sceneSave = new string[13+1];
        sceneInfo.characters = new();
        sceneInfo.Regina = false;

        //Set Spawnpoint
        sceneInfo.spawnpoint = new Vector3(-7.19f, -2f, 0f);
        sceneInfo.spawnpointRotation = Quaternion.identity;

        ///End Start Button Stuff

        SceneManager.LoadScene("Piratenstadt");
    }

}
