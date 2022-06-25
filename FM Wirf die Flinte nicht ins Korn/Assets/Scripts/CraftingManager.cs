using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftingManager : MonoBehaviour
{
    private Controller controller;
    public string[] recipes;
    public ScrItem[] results;

    private void Start()
    {
        controller = gameObject.GetComponent<Controller>();
    }



    public bool Craft(string precipe, int pslot)
    {
        Debug.Log(precipe);
        Debug.Log(pslot);
        for (int r = 0; r < recipes.Length; r++)
        {
            if(recipes[r] == precipe)
            {
                controller.inventory.isFull[pslot] = false;
                controller.inventory.content[pslot] = null;
                for (int i = 0; i < controller.inventory.slots.Length; i++)
                {
                    if (controller.inventory.isFull[i] == false)
                    {
                        //fills the content array with the ScrItem
                        controller.inventory.isFull[i] = true;
                        Instantiate(results[r].UIObject, controller.inventory.slots[i].transform, false);
                        controller.inventory.content[i] = results[r];
                        return true;
                    }
                    if (i == controller.inventory.slots.Length - 1)
                    {
                        Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
                        return false;
                    }
                }
            }
        }
        return false;
    }



}
