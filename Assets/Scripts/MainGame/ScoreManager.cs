using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Dictionary<string, int> pointBins = new Dictionary<string, int>()
    {
        {"Groenlandshaj", 0},
        {"Wobbegong", 0},
        {"Sandtigerhaj", 0},
        {"Smaaplettet roedhaj", 0},
        {"Hvalhaj", 0},
    };
    [SerializeField] private int groenlandshaj;
    [SerializeField] private int wobbegong;
    [SerializeField] private int sandtigerhaj;
    [SerializeField] private int smaaplettetRoedhaj;
    [SerializeField] private int hvalhaj;

    void Awake()
    {
       //AvoidMultipleInstances();
       SceneManager.sceneLoaded += OnSceneLoaded; // (delegate) : when new scene loaded, call OnSceneLoaded
    }

    private void Update() // updates pointBins in Inspector. Using int's because you can't serialize dictionaries
    {
        groenlandshaj = pointBins["Groenlandshaj"];
        wobbegong = pointBins["Wobbegong"];
        sandtigerhaj = pointBins["Sandtigerhaj"];
        smaaplettetRoedhaj = pointBins["Smaaplettet roedhaj"];
        hvalhaj = pointBins["Hvalhaj"];
    }

    private void AvoidMultipleInstances()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ScoreManager");
        if (objs.Length > 1)
        {
            Destroy(gameObject); // Dont have multiple instances
        }
        else
        {
            DontDestroyOnLoad(gameObject); // if not multiple instances: dont destroy on load
        }
    }

    private void OnSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        if(newScene.buildIndex == 0)
        {
            pointBins["Groenlandshaj"] = 0;
            pointBins["Wobbegong"] = 0;
            pointBins["Sandtigerhaj"] = 0;
            pointBins["Smaaplettet roedhaj"] = 0;
            pointBins["Hvalhaj"] = 0;
        }
        
    }

    public void AddPointToBin(string answer)
    {
        pointBins[answer] += 1; // ... add 1 to the proper pointBin in scoreManager.
    }


    public void AddPointTest()
    {
        pointBins["Groenlandshaj"] += 1;
    }
}

