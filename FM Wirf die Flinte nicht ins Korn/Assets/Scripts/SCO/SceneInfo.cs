using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{

    //Characters

    //still needs to be resetted
    public bool Regina = false; 

    public List<ScrCharacter> characters = new List<ScrCharacter>();


    //Items

    public bool[] isFull;

    public ScrItem[] content;

    //SceneItems

    public string[] sceneSave = new string[13];

    //SpawnPos
    public Vector3 spawnpoint;



}
