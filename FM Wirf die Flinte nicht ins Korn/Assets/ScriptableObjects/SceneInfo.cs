using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{

    public bool Regina = false;

    public List<ScrCharacter> characters = new List<ScrCharacter>();    



}
