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

                /*giving a Character an Item - has been moved down since 
                 * pcombineElement.GetComponent<Character>().character.itemRecieved
                 * is needed to determine if results[r] should be given before changing itemRecieved to true
                 */

                //combining an Item on Scene
                if (pcombineElement.GetComponent<Item>() != null)
                {
                    //removes pdragElement
                    RemoveFromInventoryAndDestroy(pdragElement);
                    Debug.Log(precipe);
                    //insert here
                }

                ///End of Section for Special Combines

                //fills inventory with results Item
                if (pcombineElement.GetComponent<NoReturn>() == null)
                {
                    //when pcombineElement is a character & has not recieved the results[r] item
                    if (pcombineElement.GetComponent<Character>() != null && !pcombineElement.GetComponent<Character>().character.itemRecieved)
                    {
                        //removes pdragElement
                        RemoveFromInventoryAndDestroy(pdragElement);
                        //adds result Item
                        AddThisResultsItemAt(r, slotIndex);
                    }
                    //when the pcombineElement is not a Character
                    else if(pcombineElement.GetComponent<Character>() == null)
                    {
                        for (int i = 0; i < controller.inventory.slots.Length; i++)
                        {
                            if (controller.inventory.isFull[i] == false)
                            {
                                //adds result Item
                                AddThisResultsItemAt(r, i);
                                break;
                            }
                            if (i == controller.inventory.slots.Length - 1)
                            {
                                Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
                            }
                        }
                    }
                }
                else
                {
                    RemoveFromInventoryAndDestroy(pdragElement);
                }

                //giving a Character an Item
                if (pcombineElement.GetComponent<Character>() != null)
                {
                    controller.isDragging = false;
                    pcombineElement.GetComponent<Character>().ReactToClick(controller, pdragElement);
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
