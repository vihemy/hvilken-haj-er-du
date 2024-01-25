using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class MailchimpAPI : Singleton<MailchimpAPI>
{
    private static string dataCenter;
    private static string apiKey;
    private static string listId;

    private void Start()
    {
        dataCenter = ConfigLoader.Instance.LoadFromConfig("DATA_CENTER");
        apiKey = ConfigLoader.Instance.LoadFromConfig("API_KEY");
        listId = ConfigLoader.Instance.LoadFromConfig("LIST_ID");
    }

    public void AddSubscriber(string email, string fname, string lname)
    {
        string requestJson = CreateSubscriberJson(email, fname, lname);
        StartCoroutine(AddSubscriberViaAPI(dataCenter, $"lists/{listId}/members", requestJson, apiKey));
    }

    private string CreateSubscriberJson(string email, string fname, string lname)
    {
        // prepares nested object
        MergeFields merge_fields = new MergeFields
        {
            FNAME = fname,
            LNAME = lname, // Set LNAME value
            GDPR = "Ja tak, jeg bekræfter, at jeg vil modtage e-mailmarkedsføring fra Kattegatcentret",
            KONKBET = "Jeg accepterer konkurrencebetingelserne"
        };

        // prepares outer object
        var sub = new SubscriberInfo
        {
            email_address = email,
            status = "subscribed",
            merge_fields = merge_fields,
            tags = new List<string> { "SCN 2024" }
        };
        string json = JsonConvert.SerializeObject(sub, Formatting.Indented);
        return json;
    }

    private IEnumerator AddSubscriberViaAPI(string dataCenter, string method, string requestJson, string key)
    {
        var request = CreateMailchimpRequest(dataCenter, method, requestJson, key);
        yield return SendRequest(request);

        HandleResponse(request, requestJson);
    }

    private UnityWebRequest CreateMailchimpRequest(string dataCenter, string method, string requestJson, string key)
    {
        string endpoint = $"https://{dataCenter}.api.mailchimp.com/3.0/{method}";
        var request = new UnityWebRequest(endpoint, "POST");
        SetupRequestBody(request, requestJson);
        SetupRequestHeaders(request, key);
        return request;
    }

    private void SetupRequestBody(UnityWebRequest request, string requestJson)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestJson);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
    }

    private void SetupRequestHeaders(UnityWebRequest request, string key)
    {
        request.SetRequestHeader("Content-Type", "application/json");
        string authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"anystring:{key}"));
        request.SetRequestHeader("Authorization", "Basic " + authHeader);
    }

    private IEnumerator SendRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
    }

    private void HandleResponse(UnityWebRequest request, string requestJson)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            Debug.LogError(requestJson);
            Debug.LogError(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Response from Mailchimp: " + request.downloadHandler.text);
        }
    }
}

public class SubscriberInfo
{
    public string email_address { get; set; }
    public string status { get; set; }
    public MergeFields merge_fields { get; set; }
    public List<string> tags { get; set; }
}

public class MergeFields
{
    public string FNAME { get; set; }
    public string LNAME { get; set; }
    public string GDPR { get; set; }
    public string KONKBET { get; set; }
}
