using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public SceneInfo sceneInfo;
    public GameObject startButton;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }



    public void StartTheGame()
    {
        ///Start Button Stuff:

        //resets Ending-Logic
        EndGame.flintew = false;
        EndGame.flintel = false;
        EndGame.TS = false;

       //resets sceneInfo
        sceneInfo.tavernenScore = 0;
        sceneInfo.plantScore = 0;
        sceneInfo.plantHasGrown = false;
        sceneInfo.isFull = new bool[13];
        sceneInfo.content = new ScrItem[13];
        sceneInfo.sceneSave = new string[13+1];
        sceneInfo.characters = new();
        sceneInfo.Regina = false;
        sceneInfo.tavernenScore = 0;
        sceneInfo.Flintendialog = false;

        //Set Spawnpoint
        sceneInfo.spawnpoint = new Vector3(-7.19f, -2f, 0f);
        sceneInfo.spawnpointRotation = Quaternion.identity;

        //Intantiates
        sceneInfo.toInstantiateItem = new GameObject[5];
        sceneInfo.itemsSpawnPos = new Vector3[5];
        sceneInfo.sceneItemLaysIn = new string[5];

        //visited Scenes
        sceneInfo.visitedScenes = new string[6];
        sceneInfo.previousScene = "Start";
        ///End Start Button Stuff

    //Audio
        audioManager.Play("Write Book");

        SceneManager.LoadScene("Piratenstadt");
    }

    public void StartCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BackToStartMenue()
    {
        //Audio
        audioManager.Play("Write Book");

       // controller.controllsMenue.SetActive(false);
        //controller.pauseMenue.SetActive(true);
    }


}
