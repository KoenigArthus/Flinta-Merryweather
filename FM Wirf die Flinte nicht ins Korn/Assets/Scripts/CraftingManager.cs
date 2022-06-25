using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public string[] recipe;
    public ScrItem[] results;

    public void Craft(string precipe)
    {
        Debug.Log(precipe);
    }



}
