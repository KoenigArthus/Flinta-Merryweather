using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "SceneInfo")]
public class SceneInfo : ScriptableObject
{

    //Characters
    public bool Regina = false;
    public bool Flintendialog = false;
    public List<ScrCharacter> characters = new();


    //Items
    public bool[] isFull;
    public ScrItem[] content;

    //SceneItems
    public string[] sceneSave = new string[13];

    //Spawnpoint
    public Vector3 spawnpoint;
    public Quaternion spawnpointRotation;



}
