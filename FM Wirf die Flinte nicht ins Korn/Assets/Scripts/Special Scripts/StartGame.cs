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

        //Intantiates
        sceneInfo.toInstantiateItem = new GameObject[5];
        sceneInfo.itemsSpawnPos = new Vector3[5];
        sceneInfo.sceneItemLaysIn = new string[5];

        //visited Scenes
        sceneInfo.visitedScenes = new string[6];
        ///End Start Button Stuff

        SceneManager.LoadScene("Piratenstadt");
    }

}
