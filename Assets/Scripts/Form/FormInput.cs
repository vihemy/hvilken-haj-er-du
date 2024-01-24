// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System;
// using System.Runtime.CompilerServices;

// public class FormInput : Singleton<FormInput>
// {
//     [SerializeField] private TMP_InputField emailInputField;
//     [SerializeField] private TMP_InputField fnameInputField;
//     [SerializeField] private TMP_InputField lnameInputField;

//     void Start()
//     {
//         ResetInputField();
//     }

//     public void AddMember()
//     {
//         if (!IsFieldEmpty())
//         {
//             MemberInfo memberInfo = CreateMemberInfo();
//             MailchimpAPICaller.Instance.AddMember(memberInfo);
//             Debug.Log($"Email: {memberInfo.email_address}, First Name: {memberInfo.merge_fields.FNAME}, Last Name: {memberInfo.merge_fields.LNAME}");
//             ResetInputField();
//         }
//     }

//     private MemberInfo CreateMemberInfo()
//     {
//         string email = emailInputField.text;
//         string fname = fnameInputField.text;
//         string lname = lnameInputField.text;

//         MemberInfo info = new MemberInfo();
//         info.email_address = email;
//         info.merge_fields.FNAME = fname;
//         info.merge_fields.LNAME = lname;

//         return info;
//     }

//     private bool IsFieldEmpty()
//     {
//         return string.IsNullOrEmpty(emailInputField.text) || string.IsNullOrEmpty(fnameInputField.text) || string.IsNullOrEmpty(lnameInputField.text);
//     }

//     private void ResetInputField()
//     {
//         emailInputField.text = "";
//         fnameInputField.text = "";
//         lnameInputField.text = "";
//         emailInputField.Select();
//     }
// }
