using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;


public class SkipScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (LocalizationSettings.SelectedLocale.LocaleName != "Danish (da)")
        {
            SceneLoader.Instance.LoadNextScene();
        }
    }
}
