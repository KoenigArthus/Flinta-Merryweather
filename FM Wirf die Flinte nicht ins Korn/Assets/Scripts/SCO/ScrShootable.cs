using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shootable", menuName = "Shootable")]
public class ScrShootable : ScriptableObject
{
    [Header("Description")]
    [TextArea] public string viewText;

    [Header("Interaction Variables")]
    public bool falls;
    public bool despawns;
    public bool remains;



    public int fallHight;
    public GameObject fallItem;
    public Vector3 Spawn = new Vector3(1, 1, 1);

    [Header("Sprites")]
    public Sprite sprite;

    //[Header("Sounds")]
   

}
