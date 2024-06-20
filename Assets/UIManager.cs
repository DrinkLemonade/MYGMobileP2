using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    VisualElement root;
    TextField emailField;
    Button button;

    [SerializeField]
    GameObject UIDoc;
    public static Action OnButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        root = UIDoc.GetComponent<UIDocument>().rootVisualElement;
        emailField = root.Q<TextField>("EmailField");
        button = root.Q<Button>("Button");
        button.clickable.clicked += OnClick; //TODO: Don't allow it to stack
        Debug.Log(button.name);
    }

    void OnClick()
    {
    }
}
