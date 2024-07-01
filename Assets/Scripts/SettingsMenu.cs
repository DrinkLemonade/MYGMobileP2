using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WebSocketSharp;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var container = GetComponent<UIDocument>().rootVisualElement;

        // Get a reference to the slider from UXML and assign it its value.
        var uxmlSlider = container.Q<Slider>("the-uxml-slider");
        uxmlSlider.value = 42.2f;

        uxmlSlider.RegisterValueChangedCallback(SliderChanged);
    }

    void SliderChanged(ChangeEvent<float> ev)
    {
        AudioManager.i.musicVolume = ev.newValue;
    }
}
