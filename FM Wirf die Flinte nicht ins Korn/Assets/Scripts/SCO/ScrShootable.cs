using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shootable", menuName = "Shootable")]
public class ScrShootable : ScriptableObject
{
    [Header("Interaction Variables")]
    public bool falls;
    public bool despawns;
    public bool hasFallen;
    [Header("Sprites")]
    public Sprite sprite;

    //[Header("Sounds")]
   

}
