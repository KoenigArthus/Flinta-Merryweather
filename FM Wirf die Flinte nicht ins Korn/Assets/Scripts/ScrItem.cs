using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ScrItem : ScriptableObject
{
    public bool isViewable;
    public bool canBePickedUp;
    public bool canBeCombined;

    public Sprite sprite;
    public GameObject UIObject;
}
