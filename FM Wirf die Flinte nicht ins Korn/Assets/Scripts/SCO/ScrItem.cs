using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ScrItem : ScriptableObject
{
    [Header("Description")]
    [TextArea] public string viewText;

    [Header("Interaction Variables")]
    public bool isViewable;
    public bool canBePickedUp;
    public bool canBeCombined;

    [Header("Sprites")]
    public Sprite sprite;
    public GameObject UIObject;
}
