using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    VisualElement root;
    TextField tf;
    Button button;

    [SerializeField]
    GameObject UIDoc;
    public static Action OnButtonClicked;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        root = UIDoc.GetComponent<UIDocument>().rootVisualElement;
        tf = root.Q<TextField>("EmailField");
        button = root.Q<Button>("Button");
        button.clicked += OnButtonClickedHandler; //TODO: Don't allow it to stack
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonClickedHandler()
    {
        Debug.Log("button clicked!");
    }
}
