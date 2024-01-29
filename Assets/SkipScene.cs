using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;


public class SkipScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string currentScene = SceneLoader.Instance.GetCurrentSceneName();
        if (currentScene == "Form")
        {
            SceneLoader.Instance.LoadNextScene();
        }
    }
}
