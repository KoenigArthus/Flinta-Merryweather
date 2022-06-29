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
    public void Craft(string precipe, GameObject pdragElement, GameObject pcombineElement)
    {
        Debug.Log(precipe);
        for (int r = 0; r < recipes.Length; r++)
        {
            if (recipes[r] == precipe)
            {
                //instantiating local variables
                string[] lslotNameParts = pdragElement.transform.parent.name.Split('(', ')');
                int lslotInt = new();

                ///Start of Section for Special Combines

                //if the combine element is an UIItem then it will be removed from the inventory & destroyed
                if (pcombineElement.GetComponent<Dragger>() != null)
                {
                    lslotNameParts = pcombineElement.transform.parent.name.Split('(', ')');
                    lslotInt = int.Parse(lslotNameParts[1]);
                    controller.inventory.isFull[lslotInt] = false;
                    controller.inventory.content[lslotInt] = null;
                    Destroy(pcombineElement);
                }

                /*giving a Character an Item - has been moved down since 
                 * pcombineElement.GetComponent<Character>().character.itemRecieved
                 * is needed to determin what if pdragElement should be given instead
                 * thus when talking with a chracter the new item first gets added then the bool itemRecieved set to true
                 */

                //combining an Item on Scene
                if (pcombineElement.GetComponent<Item>() != null)
                {
                    Debug.Log(precipe);
                    //insert here
                }

                ///End of Section for Special Combines

                //removes the pdragItem item from the inventory & destroys it
                lslotNameParts = pdragElement.transform.parent.name.Split('(', ')');
                lslotInt = int.Parse(lslotNameParts[1]);
                controller.inventory.isFull[lslotInt] = false;
                controller.inventory.content[lslotInt] = null;
                Destroy(pdragElement);

                //fills inventory with results Item
                if (pcombineElement.GetComponent<NoReturn>() == null)
                {
                    for (int i = 0; i < controller.inventory.slots.Length; i++)
                    {
                        if (controller.inventory.isFull[i] == false)
                        {
                            //for pontentially reseting
                            ScrItem lscrItem = results[r];
                            //returning the pdragElement when a character already recieved the correct Item
                            if (pcombineElement.GetComponent<Character>() != null && pcombineElement.GetComponent<Character>().character.itemRecieved)
                            {
                                results[r] = pdragElement.GetComponent<Dragger>().scrItem;
                            }
                            results[r].UIObject.GetComponent<Dragger>().scrItem = results[r];
                            Instantiate(results[r].UIObject, controller.inventory.slots[i].transform, false);
                            controller.inventory.isFull[i] = true;
                            controller.inventory.content[i] = results[r];
                            //reseting results[r] to avoid reseting it somethere else
                            results[r] = lscrItem;
                            break;
                        }
                        if (i == controller.inventory.slots.Length - 1)
                        {
                            Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
                        }
                    }
                }
                else
                {
                    //nichts
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
}
