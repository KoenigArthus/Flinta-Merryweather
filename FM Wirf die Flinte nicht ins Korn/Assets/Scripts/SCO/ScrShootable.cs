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

    public int fallHight;

    [Header("Sprites")]
    public Sprite sprite;

    //[Header("Sounds")]
   

}
