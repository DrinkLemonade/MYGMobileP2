using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginMenu : MonoBehaviour
{
    VisualElement root, loginWindow;
    TextField emailField, passwordField;
    Button button;
    Label emailWarning, passwordWarning;

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

    public static Action OnButtonClicked;

    [SerializeField]
    string shownIfEmpty = "AAAAAAAAAAAAAH";

    [SerializeField]
    AudioClip onClicked;

    void Start()
    {
        //Get
        root = GetComponent<UIDocument>().rootVisualElement;
        loginWindow = root.Q<VisualElement>("WindowContainer");
        emailField = root.Q<TextField>("EmailField");
        passwordField = root.Q<TextField>("PasswordField");

        emailWarning = root.Q<Label>("EmailWarningLabel");
        passwordWarning = root.Q<Label>("PasswordWarningLabel");
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
        Debug.Log($"running SetEmpty for {field.name}, active: {active}");
        field.style.unityFontStyleAndWeight = active ? FontStyle.Italic : FontStyle.Normal;
        field.SetValueWithoutNotify(active ? shownIfEmpty : "");
    }

    void OnFieldClickEvent(ClickEvent ev, TextField field)
    {
        if (field.text == shownIfEmpty) SetEmptyDefaultText(field, false);
        if (field.name == "PasswordField") field.isPasswordField = true;
    }
    void OnFieldStringChangedEvent(ChangeEvent<string> ev, TextField field)
    {
        if (ev.newValue == "") SetEmptyDefaultText(field, false);
        //TODO: For some reason this isn't working but it wasn't really requested, soooo
        else field.style.unityFontStyleAndWeight = FontStyle.Normal;
    }

    void OnButtonClick()
    {
        if (validated) return;
        AudioManager.i.PlaySound(onClicked);

        Debug.Log($"button clicked! Contents are: {emailField.text}, {passwordField.text}");
        bool okMail = false, okPassword = false;
        okMail = (emailField.value != "");
        if (okMail) okMail = EmailIsValid(emailField.value);
        okPassword = (passwordField.value != "" && passwordField.value != shownIfEmpty);
        emailWarning.visible = !okMail;
        passwordWarning.visible = !okPassword;
        if (okMail && okPassword) ValidateLogin();
    }

    void ValidateLogin()
    {
        if (easeCurve != null && useCurveInsteadOfEase) DOTween.To(() => validateSlide, x => validateSlide = x, -110, easeTimeSeconds).SetEase(easeCurve);
        else DOTween.To(() => validateSlide, x => validateSlide = x, -110, easeTimeSeconds).SetEase(easeType);
    }

    private void Update()
    {
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
