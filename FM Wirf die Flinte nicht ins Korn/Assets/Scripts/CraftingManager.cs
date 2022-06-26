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



    public bool Craft(string precipe, GameObject pdragItem)
    {
        Debug.Log(precipe);
        for (int r = 0; r < recipes.Length; r++)
        {
            // if the given item combination matches the recipe...
            // ... the pdragItem gets removed and the matching results Item put into the Inventory
            if(recipes[r] == precipe)
            {
                //removes the initial combination item
                string[] lslotNameParts = pdragItem.transform.parent.name.Split('(', ')');
                int ldragItemSlot = int.Parse(lslotNameParts[1]);
                Destroy(pdragItem);
                controller.inventory.isFull[ldragItemSlot] = false;
                controller.inventory.content[ldragItemSlot] = null;
                for (int i = 0; i < controller.inventory.slots.Length; i++)
                {
                    if (controller.inventory.isFull[i] == false)
                    {
                        //fills the content array with the ScrItem
                        controller.inventory.isFull[i] = true;
                        results[r].UIObject.GetComponent<Dragger>().scrItem = results[r];
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
