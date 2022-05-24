using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thing", menuName = "Thing")]
public class ScriptableThing : ScriptableObject
{
    public bool isViewable;
    public bool isPlacable;
    public bool canBePickedUp;
    public bool canBeCombined;

    public GameObject UIObject;
}
