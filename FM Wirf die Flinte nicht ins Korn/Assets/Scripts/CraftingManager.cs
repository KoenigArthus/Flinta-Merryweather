using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private Controller controller;
    public string[] recipes;
    public ScrItem[] results;

    private void Start()
    {
        controller = gameObject.GetComponent<Controller>();
    }


    // if the given item combination matches the recipe...
    // ... the pdragItem gets removed and the matching results Item put into the Inventory
    public bool Craft(string precipe, GameObject pdragElement, GameObject pcombineElement)
    {
        Debug.Log(precipe);
        for (int r = 0; r < recipes.Length; r++)
        {
            if(recipes[r] == precipe)
            {
                //removes the pdragItem item from the inventory & destroys it
                string[] lslotNameParts = pdragElement.transform.parent.name.Split('(', ')');
                int lslotInt = int.Parse(lslotNameParts[1]);
                Destroy(pdragElement);
                controller.inventory.isFull[lslotInt] = false;
                controller.inventory.content[lslotInt] = null;

                //if true removes the combine Element item from the inventory & destroys it
                if(pcombineElement.transform.parent.GetComponent<Dragger>() != null)
                {
                    lslotNameParts = pcombineElement.transform.parent.name.Split('(', ')');
                    lslotInt = int.Parse(lslotNameParts[1]);
                    Destroy(pcombineElement);
                    controller.inventory.isFull[lslotInt] = false;
                    controller.inventory.content[lslotInt] = null;
                }
                else
                {
                    Destroy(pcombineElement);
                }
                //fillst inventory with results Item
                for (int i = 0; i < controller.inventory.slots.Length; i++)
                {
                    if (controller.inventory.isFull[i] == false)
                    {
                        results[r].UIObject.GetComponent<Dragger>().scrItem = results[r];
                        Instantiate(results[r].UIObject, controller.inventory.slots[i].transform, false);
                        controller.inventory.isFull[i] = true;
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

    // if the given item combination matches the recipe...
    // ... the pdragItem gets removed and the matching results Item put into the Inventory
    public bool Craft(string precipe, GameObject pdragElement)
    {
        Debug.Log(precipe);
        for (int r = 0; r < recipes.Length; r++)
        {
            if (recipes[r] == precipe)
            {
                //removes the pdragItem item from the inventory & destroys it
                string[] lslotNameParts = pdragElement.transform.parent.name.Split('(', ')');
                int lslotInt = int.Parse(lslotNameParts[1]);
                Destroy(pdragElement);
                controller.inventory.isFull[lslotInt] = false;
                controller.inventory.content[lslotInt] = null;

                //fillst inventory with results Item
                for (int i = 0; i < controller.inventory.slots.Length; i++)
                {
                    if (controller.inventory.isFull[i] == false)
                    {
                        results[r].UIObject.GetComponent<Dragger>().scrItem = results[r];
                        Instantiate(results[r].UIObject, controller.inventory.slots[i].transform, false);
                        controller.inventory.isFull[i] = true;
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
