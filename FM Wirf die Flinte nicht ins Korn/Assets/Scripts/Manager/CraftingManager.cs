using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private Controller controller;
    public string[] recipes;
    public ScrItem[] results;

    private string[] slotNameParts;
    private int slotIndex = new();


    private void Start()
    {
        controller = gameObject.GetComponent<Controller>();
    }


    // if the given item combination matches the recipe...
    // ... the pdragItem gets removed if needed and if needed the matching results Item put into the Inventory
    public void Craft(string precipe, GameObject pdragElement, GameObject pcombineElement)
    {
        Debug.Log(precipe);
        for (int r = 0; r < recipes.Length; r++)
        {
            if (recipes[r] == precipe)
            {
                ///Start of Section for Special Combines
                //if the pcombineElement is an UIItem then it & pdragElement will be removed from the inventory &destroyed
                if (pcombineElement.GetComponent<Dragger>() != null)
                {
                    //removes pdragElement
                    RemoveFromInventoryAndDestroy(pdragElement);
                    //removes pcombineElement
                    RemoveFromInventoryAndDestroy(pcombineElement);
                }

                //giving a Character an Item
                if (pcombineElement.GetComponent<Character>() != null)
                {
                    RemoveFromInventoryAndDestroy(pdragElement);
                    controller.isDragging = false;
                    if (pcombineElement.GetComponent<NoReturn>() != null)
                    {
                        pcombineElement.GetComponent<Character>().ReactToClick(controller, pdragElement);
                    }
                       
                }

                //combining an Item on Scene
                if (pcombineElement.GetComponent<Item>() != null)
                {
                    //removes pdragElement
                    RemoveFromInventoryAndDestroy(pdragElement);
                    Debug.Log(precipe);
                    //insert here
                }
                ///End of Section for Special Combines

                //fills inventory with results Item except no return
                if (pcombineElement.GetComponent<NoReturn>() == null)
                {
                    //fills inventory with results Item
                    AddThisResultsItemAt(r, slotIndex);
                }
                else
                {
                    if(pcombineElement.name == "Dieter" || 
                        pcombineElement.name == "Pflanze")
                    {
                         pcombineElement.GetComponent<NoReturn>().AnimateAction();
                    }
                }
                break;
            }
        }
    }

    private void RemoveFromInventoryAndDestroy(GameObject premoveElement)
    {
        slotNameParts = premoveElement.transform.parent.name.Split('(', ')');
        slotIndex = int.Parse(slotNameParts[1]);
        controller.inventory.isFull[slotIndex] = false;
        controller.inventory.content[slotIndex] = null;
        Destroy(premoveElement);
    }

    private void AddThisResultsItemAt(int resultsIndex, int inventoryIndex)
    {
        results[resultsIndex].UIObject.GetComponent<Dragger>().scrItem = results[resultsIndex];
        Instantiate(results[resultsIndex].UIObject, controller.inventory.slots[inventoryIndex].transform, false);
        controller.inventory.isFull[inventoryIndex] = true;
        controller.inventory.content[inventoryIndex] = results[resultsIndex];
    }

}
