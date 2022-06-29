using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class ScrCharacter : ScriptableObject
{
    [Header("Description")]
    [TextArea] public string viewText;

    [Header("Sprites")]
    public Sprite sprite;

    [Header("Dialogues")]
    public TextAsset ink;
    public TextAsset inkR;
    public TextAsset ink2;
    public TextAsset ink2R;
    public TextAsset inkIR;
    public TextAsset inkI;
    public TextAsset inkF;

    [Header("Bools")]
    public bool itemRecieved = false;







}
