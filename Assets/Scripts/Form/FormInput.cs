using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UltimateClean;

public class FormInput : Singleton<FormInput>
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField fnameInputField;
    [SerializeField] private Toggle gdprToggle;

    [SerializeField] private Toggle konkbetToggle;

    [SerializeField] private CleanButton signupButton;
    private bool isButtonOpenToSubmit = false;
    void Start()
    {
        ResetInputField();
        CloseButtonToSubmit();
    }

    void Update()
    {
        if (!isButtonOpenToSubmit)
        {
            if (IsFormComplete())
            {
                OpenButtonToSubmit();
            }
        }
        else
        {
            if (!IsFormComplete())
            {
                CloseButtonToSubmit();
            }
        }

    }

    void OpenButtonToSubmit()
    {
        signupButton.interactable = true;
        isButtonOpenToSubmit = true;
    }

    void CloseButtonToSubmit()
    {
        signupButton.interactable = false;
        isButtonOpenToSubmit = false;
    }

    public void AddMember()
    {
        if (!IsFormComplete())
        {
            Debug.Log("Alle tekstfelter skal udfyldes og alle bokse skal være markeret, for at deltage i konkurrencen");
        }
        else
        {
            MailchimpAPI.Instance.AddSubscriber(emailInputField.text, fnameInputField.text);
            ResetInputField();
        }
    }

    private bool IsFormComplete()
    {
        return AreFieldsFilledOut() && AreTogglesChecked();
    }
    private bool AreFieldsFilledOut()
    {
        return !string.IsNullOrEmpty(emailInputField.text) && !string.IsNullOrEmpty(fnameInputField.text);
    }
    private bool AreTogglesChecked()
    {
        return gdprToggle.isOn && konkbetToggle.isOn;
    }

    private void ResetInputField()
    {
        emailInputField.text = "";
        fnameInputField.text = "";
    }
}
