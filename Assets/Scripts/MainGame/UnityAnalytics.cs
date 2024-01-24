using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Collections.Generic;

public class UnityAnalytics : MonoBehaviour
{
    public static UnityAnalytics Instance { get; private set; } // uses singleton pattern to make sure only one instance of this object exists

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }
    }

    public void SendCustomEvent(string shark, bool playedTillEnd)
    {

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"sharkType", shark},
            {"playedTillEnd", playedTillEnd}

        };

        // The �gameEndedCustom� event will get queued up and sent every minute
        AnalyticsService.Instance.CustomData("gameEndedCustom", parameters);
        Debug.Log("Custom event sent. variables: " + shark + " " + playedTillEnd);
    }
}