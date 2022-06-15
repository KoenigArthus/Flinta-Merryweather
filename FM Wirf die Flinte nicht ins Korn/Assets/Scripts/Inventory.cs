using UnityEngine;

public class Inventory : MonoBehaviour
{
    //showing which slot is full
    public bool[] isFull;

    //reference to the slots
    public GameObject[] slots;

    //saves what Items have been stored
    public ScrItem[] content;

}
