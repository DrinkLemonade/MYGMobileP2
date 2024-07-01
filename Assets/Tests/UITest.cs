using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using NUnit.Framework;

public class UITest : MonoBehaviour
{
    GameObject holderGameObject;
    UIDocument loginUiDoc;
    Button loginButton;
    Label emailWarning, passwordWarning;
    TextField emailField, passwordField;

    void SetUpTest()
    {
        holderGameObject = GameObject.Find("UIDocumentMainMenu");
        loginUiDoc = holderGameObject.GetComponent<UIDocument>();
        loginButton = loginUiDoc.rootVisualElement.Q<Button>();

        emailWarning = loginUiDoc.rootVisualElement.Q<Label>("EmailWarningLabel");
        passwordWarning = loginUiDoc.rootVisualElement.Q<Label>("PasswordWarningLabel");
    }

    [UnityTest]
    public IEnumerator CheckErrorLabelsHidden()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        Assert.IsFalse(emailWarning.visible);
        Assert.IsFalse(passwordWarning.visible);
    }

    [UnityTest]
    public IEnumerator CheckButtonClicked()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);
    }

    [UnityTest]
    public IEnumerator CheckLoginFieldsErrors()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);
        yield return null;

        Assert.IsTrue(emailWarning.visible);
        Assert.IsTrue(passwordWarning.visible);
    }

    [UnityTest]
    public IEnumerator CheckLoginAcceptable()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        var emailField = loginUiDoc.rootVisualElement.Q<TextField>("EmailField");
        var passwordField = loginUiDoc.rootVisualElement.Q<TextField>("PasswordField");

        emailField.value = "test@test.com";
        passwordField.value = "mycoolpassword";

        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);
        yield return null;

        Assert.IsFalse(emailWarning.visible);
        Assert.IsFalse(passwordWarning.visible);
    }
}
