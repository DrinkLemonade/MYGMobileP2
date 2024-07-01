using UnityEngine;

//Code and comments from https://docs.unity3d.com/Manual/UIE-HowTo-CreateRuntimeUI.html
public enum ECharacterClass
{
    Knight, Ranger, Wizard
}

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public string CharacterName;
    public ECharacterClass Class;
    public Sprite PortraitImage;
}