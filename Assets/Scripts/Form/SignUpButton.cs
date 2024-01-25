using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpButton : MonoBehaviour
{
    void Start()
    {
        // using in-script listener to better find in search
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClickCallback(); });
    }

    private void OnClickCallback()
    {
        FormInput.Instance.AddMember();
        SceneLoader.Instance.LoadNextScene();
    }
}
