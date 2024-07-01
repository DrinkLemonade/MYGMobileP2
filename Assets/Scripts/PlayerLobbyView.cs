using UnityEngine;
using UnityEngine.UIElements;

//Code and comments from https://docs.unity3d.com/Manual/UIE-HowTo-CreateRuntimeUI.html

public class MainView : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;

    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        // Initialize the character list controller
        var characterListController = new PlayerListController();
        characterListController.InitializeCharacterList(uiDocument.rootVisualElement, m_ListEntryTemplate);
    }
}