using UnityEngine.UIElements;

//Code and comments from https://docs.unity3d.com/Manual/UIE-HowTo-CreateRuntimeUI.html

public class PlayerListEntryController
{
    Label m_NameLabel;

    // This function retrieves a reference to the 
    // Player name label inside the UI element.
    public void SetVisualElement(VisualElement visualElement)
    {
        m_NameLabel = visualElement.Q<Label>("character-name");
    }

    // This function receives the Player whose name this list 
    // element is supposed to display. Since the elements list 
    // in a `ListView` are pooled and reused, it's necessary to 
    // have a `Set` function to change which Player's data to display.
    public void SetPlayerData(CharacterData PlayerData)
    {
        m_NameLabel.text = PlayerData.CharacterName;
    }
}