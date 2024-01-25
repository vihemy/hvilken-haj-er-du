using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class MailchimpAPI : MonoBehaviour
{
    private static string dataCenter;
    private static string apiKey;
    private static string listId;

    private void Start()
    {
        dataCenter = ConfigLoader.Instance.LoadFromConfig("DATA_CENTER");
        apiKey = ConfigLoader.Instance.LoadFromConfig("API_KEY");
        listId = ConfigLoader.Instance.LoadFromConfig("LIST_ID");
        // AddSubscriber("test7@testesen.dk", "Test", "Testesen");
    }

    public void AddSubscriber(string email, string fname, string lname)
    {
        string requestJson = CreateSubscriberJson(email, fname, lname);
        AddSubscribertViaAPI(dataCenter, $"lists/{listId}/members", requestJson, apiKey);
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


    private static void AddSubscribertViaAPI(string dataCenter, string method, string requestJson, string key)
    {
        string endpoint = $"https://{dataCenter}.api.mailchimp.com/3.0/{method}";
        byte[] dataStream = Encoding.UTF8.GetBytes(requestJson);
        WebRequest request = WebRequest.Create(endpoint);
        try
        {
            request.ContentType = "application/json";
            SetBasicAuthHeader(request, "anystring", key); // BASIC AUTH
            request.Method = "POST";
            request.ContentLength = dataStream.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();

            WebResponse response = request.GetResponse();
            response.Close();
        }
        catch (WebException ex)
        {
            Debug.LogError(ex.Message);
            Debug.LogError(requestJson);

            Stream responseStream = ex.Response?.GetResponseStream();
            if (responseStream != null)
            {
                using StreamReader sr = new StreamReader(responseStream);
                Debug.LogError(sr.ReadToEnd());
            }
        }
    }

    private static void SetBasicAuthHeader(WebRequest request, string username, string password)
    {
        string auth = $"{username}:{password}";
        auth = Convert.ToBase64String(Encoding.Default.GetBytes(auth));
        request.Headers["Authorization"] = "Basic " + auth;
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
}
