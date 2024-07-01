using DG.Tweening;
using System;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginMenu : MonoBehaviour
{
    //UI Elements
    VisualElement root, loginWindow;
    TextField emailField, passwordField;
    Button button;
    Label emailWarning, passwordWarning;

    //Validating the login credentials and moving the window afterwards
    bool validated = false;
    float validateSlide = 0;
    [SerializeField]
    Ease easeType;
    [SerializeField]
    AnimationCurve easeCurve;
    [SerializeField]
    float easeTimeSeconds = 2f;
    [SerializeField]
    bool useCurveInsteadOfEase = false;

    //Shown in text fields when they are empty.
    [SerializeField]
    string shownIfEmpty = "Overwrite this value in editor.";

    //Button clicking audio
    [SerializeField]
    AudioClip onClicked;

    void Start()
    {
        //Get UIElements
        root = GetComponent<UIDocument>().rootVisualElement;
        loginWindow = root.Q<VisualElement>("WindowContainer");
        emailField = root.Q<TextField>("EmailField");
        passwordField = root.Q<TextField>("PasswordField");
        emailWarning = root.Q<Label>("EmailWarningLabel");
        passwordWarning = root.Q<Label>("PasswordWarningLabel");

        //Hide warnings
        emailWarning.visible = false;
        passwordWarning.visible = false;

        //Email
        emailField.RegisterCallback<ChangeEvent<string>, TextField>(OnFieldStringChangedEvent, emailField);
        emailField.RegisterCallback<ClickEvent, TextField>(OnFieldClickEvent, emailField);
        SetEmptyDefaultText(emailField, true);

        //Password
        passwordField.RegisterCallback<ChangeEvent<string>, TextField>(OnFieldStringChangedEvent, passwordField);
        passwordField.RegisterCallback<ClickEvent, TextField>(OnFieldClickEvent, passwordField);
        SetEmptyDefaultText(passwordField, true);

        //Login button
        button = root.Q<Button>("Button");
    }

    private void OnEnable()
    {
        //Re-establish the button's on-click action
        root = GetComponent<UIDocument>().rootVisualElement;
        button = root.Q<Button>("Button");
        button.clickable.clicked += OnButtonClick;
    }
    private void OnDisable()
    {
        button.clickable.clicked -= OnButtonClick;
    }

    void SetEmptyDefaultText(TextField field, bool active)
    {
        //If true: the chosen text field will display the string it must display when nothing has been entered.
        //If false, display nothing.
        Debug.Log($"running SetEmpty for {field.name}, active: {active}");
        field.style.unityFontStyleAndWeight = active ? FontStyle.Italic : FontStyle.Normal;
        field.SetValueWithoutNotify(active ? shownIfEmpty : "");
    }

    void OnFieldClickEvent(ClickEvent ev, TextField field)
    {
        //If the default "enter text" string is being displayed, remove it.
        //Also, change the password field to password mode so it displays asterisks.
        if (field.text == shownIfEmpty) SetEmptyDefaultText(field, false);
        if (field.name == "PasswordField") field.isPasswordField = true;
    }
    void OnFieldStringChangedEvent(ChangeEvent<string> ev, TextField field)
    {
        //TODO: Not used, but I could play a 'typing' sound clip here.
    }

    void OnButtonClick()
    {
        //Skip if we've validated the login and started playing the window movement animation.
        if (validated) return;
        AudioManager.i.PlaySound(onClicked);

        Debug.Log($"button clicked! Contents are: {emailField.text}, {passwordField.text}");

        //Is the email field non-empty?
        bool okMail = emailField.value != "";
        //If yes, we can check if it's valid. (Checking an empty string would cause an exception.)
        if (okMail) okMail = EmailIsValid(emailField.value);
        //Is the password field non-empty, and also NOT the default "enter your password" text?
        bool okPassword = passwordField.value != "" && passwordField.value != shownIfEmpty;
        
        //Show the error message labels if needed.
        emailWarning.visible = !okMail;
        passwordWarning.visible = !okPassword;

        //If the email and password are valid, the login goes through.
        if (okMail && okPassword) ValidateLogin();
    }

    void ValidateLogin()
    {
        //Do we have a custom animation curve set up in the inspector? If yes, use it with DOTween to slide offscreen (-110% from default position).
        if (easeCurve != null && useCurveInsteadOfEase) DOTween.To(() => validateSlide, x => validateSlide = x, -110, easeTimeSeconds).SetEase(easeCurve);
        //If not, use the ease type selected in the inspector dropdown.
        else DOTween.To(() => validateSlide, x => validateSlide = x, -110, easeTimeSeconds).SetEase(easeType);
    }

    private void Update()
    {
        //Adjust position if we're making the window slide off-screen.
        loginWindow.style.top = Length.Percent(validateSlide);
    }

    public bool EmailIsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
