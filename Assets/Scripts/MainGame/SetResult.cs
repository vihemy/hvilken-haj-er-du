using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SetResult : MonoBehaviour
{
    private ScoreManager scoreManager;
    private StatManager statManager;
    private BarChart barChart;
    private string resultKey;
    private int resultIndex;


    // [SerializeField] private TextMeshProUGUI headerGO;
    // [SerializeField] private TextMeshProUGUI bodyGO;
    // [SerializeField] private Image imageGO;
    // public List<ResultsUIData> resultsUIData;

    [SerializeField] private GameObject smaaplettetRoedhajGO;
    [SerializeField] private GameObject sandtigerhajGO;
    [SerializeField] private GameObject wobbegongGO;
    [SerializeField] private GameObject groenlandshajGO;
    [SerializeField] private GameObject hvalhajGO;






    void Start()
    {
        scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        statManager = GameObject.FindWithTag("StatManager").GetComponent<StatManager>();
        barChart = GameObject.FindWithTag("StatDisplay").GetComponentInChildren<BarChart>();

        resultKey = FindMaxKeyInPointBins(); //finds Key with highest value in pointBins
        statManager.AddDataToStats(resultKey); //sends result to statManager
        ActivateApropriateUI(resultKey); // activates appropriate UI
        barChart.UpdateBarchart(); // updates bar chart
        UnityAnalytics.Instance.SendCustomEvent(resultKey, true); // sends custom event to Unity Analytics

        //    resultIndex = GetIndexOfResultKey(resultKey); // sets an index correponsing to resultKey
        //    SetUIWithAppropriateResultData(resultKey); // sets appropriate UI-content according to resultIndex

    }

    public string FindMaxKeyInPointBins() // loops through pointBins. Sets variables maxValue & maxKey to highest item.Value & item.Key. Returns maxKey
    {
        int maxValue = 0;
        string maxKey = "";
        foreach (var item in scoreManager.pointBins)
        {
            if (item.Value > maxValue)
            {
                maxValue = item.Value;
                maxKey = item.Key;
            }
        }
        return maxKey;
    }

    public void ActivateApropriateUI(string resultKey)
    {
        // activates all resuktGO's
        groenlandshajGO.SetActive(false);
        wobbegongGO.SetActive(false);
        sandtigerhajGO.SetActive(false);
        smaaplettetRoedhajGO.SetActive(false);
        hvalhajGO.SetActive(false);

        switch (resultKey)
        {
            case "Groenlandshaj":
                groenlandshajGO.SetActive(true);
                break;
            case "Wobbegong":
                wobbegongGO.SetActive(true);
                break;
            case "Sandtigerhaj":
                sandtigerhajGO.SetActive(true);
                break;
            case "Smaaplettet roedhaj":
                smaaplettetRoedhajGO.SetActive(true);
                break;
            case "Hvalhaj":
                hvalhajGO.SetActive(true);
                break;
            default:
                break;
        }
    }
    // public int GetIndexOfResultKey(string resultKey)
    // {
    //     int temp = 0;
    //     switch (resultKey)
    //     {
    //         case "Groenlandshaj":
    //             temp = 0;
    //             break;
    //         case "Wobbegong":
    //             temp = 1;
    //             break;
    //         case "Sandtigerhaj":
    //             temp = 2;
    //             break;
    //         case "Smaaplettet roedhaj":
    //             temp = 3;
    //             break;
    //         case "Hvalhaj":
    //             temp = 4;
    //             break;
    //     }
    //     return temp;
    // }

    // public void SetUIWithAppropriateResultData(int resultIndex) // old way of setting UI
    // {
    //     headerGO.text = resultsUIData[resultIndex].headerText; // sets headerText to Header GO
    //     bodyGO.text = resultsUIData[resultIndex].bodyText; // sets bodyText to body GO
    //     imageGO.sprite = resultsUIData[resultIndex].sprite; // sets sprite to appropriate content
    // }
}
