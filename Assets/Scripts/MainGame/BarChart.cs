using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using System;

//reference: https://www.youtube.com/watch?v=C1n_c5QnyDw 

public class BarChart : MonoBehaviour
{
    public Bar barPrefab;

    private List<int> inputValues; // have changed from array to list. YT-video uses array
    private StatManager statManager;

    float chartWidth;

    void Start()
    {
        statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();
        chartWidth = Screen.width + GetComponent<RectTransform>().sizeDelta.x; // get width of screen + width of bar chart. Used to control width of bars. - OBS! IF SCREEN SIZE IS CHANGED FROM 1920X1080, BARS WILL NOT BE PROPERLY SCALED!

        UpdateBarchart(); // is also called from SetResult.cs (check reference)
    }

    public void UpdateBarchart()
    {
        List<int> inputValues = ExtractDictValuesAsList();
        DisplayGraph(inputValues);
        UpdateDisplayCount(inputValues);
    }

    public List<int> ExtractDictValuesAsList()
    {
        List<int> listOfDictValues = statManager.statBins.Values.ToList<int>();
        return listOfDictValues;
    }

    // Displays the graph
    void DisplayGraph(List<int> vals)
    {
        int maxValue = vals.Max() > 0 ? vals.Max() : 1; // get max value in list using system.linq - Seems to bug if max value are 0. If so, set maxvalue to 1 to avoid division by 0

        for (int i = 0; i < vals.Count; i++)
        {
            // loops through each bar in the bar chart and sets the width of the bar to be proportional to the value of the corresponding bin

            RectTransform rectTransform = transform.GetChild(i).GetChild(0).GetComponent<RectTransform>(); // get rect transform of bar (child of child of Bar Chart-GO)

            float normalizedValue = ((float)vals[i] / (float)maxValue) * 0.98f; // converts dictvalue to normalized value (0-1) by dividing by the largest of the dictvalues. Values casted as float to get decimal diifferences. Multiplying the total width with .98 to not have the bar touching the screen-border.

            Vector2 targetSizeDelta = new Vector2(chartWidth * normalizedValue, rectTransform.sizeDelta.y);

            // lerps from old value to new value over x seconds
            StartCoroutine(LerpFunction(rectTransform, targetSizeDelta, 2));

        }
    }

    // lerp bar width from old value to new value over 1 second

    IEnumerator LerpFunction(RectTransform rectTransform, Vector2 endValue, float duration)
    {
        float time = 0;
        Vector2 startValue = rectTransform.sizeDelta;
        while (time < duration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        rectTransform.sizeDelta = endValue;
    }

    void UpdateDisplayCount(List<int> vals)
    {
        for (int i = 0; i < vals.Count; i++)
        {
            // converts string to int in order to compare to the value in the dict.
            int countInt = Convert.ToInt32(transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text);

            //plays particle effect if new value is larger.
            if (countInt < vals[i])
            {
                transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = vals[i].ToString();

                ParticleSystem particleSystem = transform.GetChild(i).GetComponentInChildren<ParticleSystem>();
                particleSystem.Stop(); //makes sure to reset particleEffect
                particleSystem.Play();
            }

        }
    }

    void DisplayGraphOld(List<int> vals)
    {
        int maxValue = vals.Max(); // get max value in list using system.linq

        for (int i = 0; i < vals.Count; i++)
        {
            // loops through each bar in the bar chart and sets the width of the bar to be proportional to the value of the corresponding bin

            RectTransform rectTransform = transform.GetChild(i).GetChild(0).GetComponent<RectTransform>(); // get rect transform of bar (child of child of Bar Chart-GO)

            float normalizedValue = ((float)vals[i] / (float)maxValue) * 0.98f; // converts dictvalue to normalized value (0-1) by dividing by the largest of the dictvalues. Values casted as float to get decimal diifferences. Multiplying the total width with .98 to not have the bar touching the screen-border.

            rectTransform.sizeDelta = new Vector2(chartWidth * normalizedValue, rectTransform.sizeDelta.y); // set width of bar (the filler element of the bar chart) to be proportional to normalizedValue
        }
    }
}


