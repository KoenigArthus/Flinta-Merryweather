using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ending", menuName = "Ending")]
public class ScrEnding : ScriptableObject
{

    [Header("Description")]
    [TextArea] public string Ending1;
    [TextArea] public string Ending2;
    [TextArea] public string Ending3;
}
