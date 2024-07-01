using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    void Start()
    {
        var container = GetComponent<UIDocument>().rootVisualElement;

        // Get a reference to the slider from UXML and assign it its value.
        var uxmlSlider = container.Q<Slider>("the-uxml-slider");
        //Arbitrary default value
        uxmlSlider.value = 42.2f;

        uxmlSlider.RegisterValueChangedCallback(SliderChanged);
    }

    void SliderChanged(ChangeEvent<float> ev)
    {
        AudioManager.i.musicVolume = ev.newValue;
    }
}
