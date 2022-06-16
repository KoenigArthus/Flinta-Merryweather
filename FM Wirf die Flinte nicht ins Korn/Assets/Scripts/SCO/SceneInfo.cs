using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{

    //Characters

    public bool Regina = false;

    public List<ScrCharacter> characters = new List<ScrCharacter>();


    //Items

    public bool[] isFull;

    public ScrItem[] content;

    //SpawnPos
    public Vector3 spawnpoint;


}
