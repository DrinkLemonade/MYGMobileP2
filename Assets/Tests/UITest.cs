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

    //Find all UIElements we'll be testing
    void SetUpTest()
    {
        holderGameObject = GameObject.Find("UIDocumentMainMenu");
        loginUiDoc = holderGameObject.GetComponent<UIDocument>();
        loginButton = loginUiDoc.rootVisualElement.Q<Button>();

        emailWarning = loginUiDoc.rootVisualElement.Q<Label>("EmailWarningLabel");
        passwordWarning = loginUiDoc.rootVisualElement.Q<Label>("PasswordWarningLabel");
    }

    //Do the error labels hide themselves properly on start?
    [UnityTest]
    public IEnumerator CheckErrorLabelsHidden()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        Assert.IsFalse(emailWarning.visible);
        Assert.IsFalse(passwordWarning.visible);
    }

    //Can we click the button without errors?
    [UnityTest]
    public IEnumerator CheckButtonClicked()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);
    }

    //Do the error labels show up when we validate a login without entering anything into the login fields?
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

    //Does the login work successfully with an appropriate email and password?
    //TODO: Technically we're just checking if entering appropriate credentials doesn't cause the labels to show up.
    //Maybe I could check if LoginMenu's "validated" is true instead.
    [UnityTest]
    public IEnumerator CheckLoginAcceptable()
    {
        SceneManager.LoadScene(0);
        yield return null;
        SetUpTest();

        //Grab the fields
        var emailField = loginUiDoc.rootVisualElement.Q<TextField>("EmailField");
        var passwordField = loginUiDoc.rootVisualElement.Q<TextField>("PasswordField");

        //Example appropriate credentials
        emailField.value = "test@test.com";
        passwordField.value = "mycoolpassword";

        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);
        yield return null;

        Assert.IsFalse(emailWarning.visible);
        Assert.IsFalse(passwordWarning.visible);
    }
}
