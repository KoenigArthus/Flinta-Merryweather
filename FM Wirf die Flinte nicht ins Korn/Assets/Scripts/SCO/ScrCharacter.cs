using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class ScrCharacter : ScriptableObject
{
    [Header("Sprites")]
    public Sprite sprite;
    [Header("Dialogues")]
    public TextAsset ink;
}
