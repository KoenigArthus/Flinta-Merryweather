using UnityEngine;

public class Character : Interactable
{
    public ScrCharacter character;

    private TextAsset ink;

    //Initializing the Character
    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = character.sprite;
        ink = character.ink;
    }
    public override void ReactToClick(Controller pcon)
    {
        //pressing the right mouse button will start a Dialogue
        if (Input.GetMouseButtonDown(1))
        {
            DialogueManager.GetInstance().EnterDialogueMode(ink, this.gameObject);
        }
    }
}
