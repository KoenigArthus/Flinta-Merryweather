using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "SceneInfo")]
public class SceneInfo : ScriptableObject
{
    //triggert Tavernenschlägerei
    [Header("World Score")]
    public int tavernenScore;
    public int plantScore;
    public bool plantHasGrown;

    [Header("Spawnpoint")]
    //Spawnpoint
    public Vector3 spawnpoint;
    public Quaternion spawnpointRotation;

    [Header("Characters")]
    //Characters
    public bool Regina = false;
    public bool Flintendialog = false;
    public List<ScrCharacter> characters = new();

    [Header("Items")]
    //Items
    public bool[] isFull;
    public ScrItem[] content;
    public string[] sceneSave = new string[15];


    [Header("For Instantiating")] 
    //To Instantiate
    public GameObject[] toInstantiateItem = new GameObject[5];
    public Vector3[] itemsSpawnPos = new Vector3[5];
    public string[] sceneItemLaysIn = new string[5];

    [Header("Visited Scenes")]
    //For visiting a Scene for a first time
    public string[] visitedScenes = new string[6];
}
