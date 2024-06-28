using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class UITest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator CheckButtonClicked()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument doc = go.GetComponent<UIDocument>();
        Button button = doc.rootVisualElement.Q<Button>();

        using (var clicked = new NavigationSubmitEvent() { target = button })
            button.SendEvent(clicked);
    }

    [UnityTest]
    public IEnumerator CheckLoginFieldsErrors()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument doc = go.GetComponent<UIDocument>();
        Button button = doc.rootVisualElement.Q<Button>();

        using (var clicked = new NavigationSubmitEvent() { target = button })
            button.SendEvent(clicked);


    }
}
