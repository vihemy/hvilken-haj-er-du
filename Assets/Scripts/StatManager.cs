using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public Dictionary<string, int> statBins = new Dictionary<string, int>()
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() // updates pointBins in Inspector. Using int's because you can't serialize dictionaries
    {
        groenlandshaj = statBins["Groenlandshaj"];
        wobbegong = statBins["Wobbegong"];
        sandtigerhaj = statBins["Sandtigerhaj"];
        smaaplettetRoedhaj = statBins["Smaaplettet roedhaj"];
        hvalhaj = statBins["Hvalhaj"];
    }

    public void AddDataToStats(string shark) // changes to UI is made in barchart.cs
    {
        if (!string.IsNullOrEmpty(shark))
        {
            statBins[shark] += 1; // ... add 1 to the proper pointBin in scoreManager.
        }
    }
}
