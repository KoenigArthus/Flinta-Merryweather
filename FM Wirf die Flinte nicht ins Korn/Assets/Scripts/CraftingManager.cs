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
            if(recipes[r] == precipe)
            {
                //removes the pdragItem item from the inventory & destroys it
                string[] lslotNameParts = pdragElement.transform.parent.name.Split('(', ')');
                int lslotInt = int.Parse(lslotNameParts[1]);
                Destroy(pdragElement);
                controller.inventory.isFull[lslotInt] = false;
                controller.inventory.content[lslotInt] = null;

                //if the combine element is an UIItem then it will be removed from the inventory & destroyed
                if (pcombineElement.GetComponent<Dragger>() != null)
                {
                    lslotNameParts = pcombineElement.transform.parent.name.Split('(', ')');
                    lslotInt = int.Parse(lslotNameParts[1]);
                    Destroy(pcombineElement);
                    controller.inventory.isFull[lslotInt] = false;
                    controller.inventory.content[lslotInt] = null;
                }


                if(pcombineElement.GetComponent<Character>() != null)
                {
                  controller.isDragging = false;
                  pcombineElement.GetComponent<Character>().ReactToClick(controller,pdragElement);
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
                        break;
                    }
                    if (i == controller.inventory.slots.Length - 1)
                    {
                        Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
                    }
                }
                break;
            }
        }
    }

  /*  // if the given item combination matches the recipe...
    // ... the pdragItem gets removed and the matching results Item put into the Inventory
    public void Craft(string precipe, GameObject pdragElement)
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

                //fills inventory with results Item
                for (int i = 0; i < controller.inventory.slots.Length; i++)
                {
                    if (controller.inventory.isFull[i] == false)
                    {
                        results[r].UIObject.GetComponent<Dragger>().scrItem = results[r];
                        Instantiate(results[r].UIObject, controller.inventory.slots[i].transform, false);
                        controller.inventory.isFull[i] = true;
                        controller.inventory.content[i] = results[r];
                        break;
                    }
                    if (i == controller.inventory.slots.Length - 1)
                    {
                        Debug.Log("Inventory is full"); //insert the text that flinta should say when the inventory is full here
                    }
                }
            }
        }
    }

*/
}
