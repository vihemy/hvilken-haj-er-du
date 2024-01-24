using UnityEngine;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

public class MailchimpAPICaller : Singleton<MailchimpAPICaller>
{
    private readonly string apiKey;
    private readonly string listId;
    private readonly string dataCenter;
    private readonly HttpClient _httpClient;

    void awake()
    {
        string dataCenter = ConfigLoader.Instance.LoadFromConfig("DATA_CENTER");
        string apiKey = ConfigLoader.Instance.LoadFromConfig("API_KEY");
        string listId = ConfigLoader.Instance.LoadFromConfig("LIST_ID");
        print(listId + apiKey + dataCenter);
    }
    private async void Start()
    {
        var apiCaller = new MailchimpAPICaller(apiKey, listId);
        bool result = await apiCaller.AddMemberAsync("example@email.com");
    }

    public MailchimpAPICaller(string apiKey, string audienceId)
    {
        apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        listId = audienceId ?? throw new ArgumentNullException(nameof(audienceId));
        _httpClient = new HttpClient();

        InitializeHttpClient();
    }

    private void InitializeHttpClient()
    {
        var baseAddress = new Uri($"https://{dataCenter}.api.mailchimp.com/3.0/");
        _httpClient.BaseAddress = baseAddress;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("anystring:" + apiKey)));
    }

    public async Task<bool> AddMemberAsync(string email)
    {
        var memberInfo = new
        {
            email_address = email,
            status = "subscribed",
            merge_fields = new
            {
                FNAME = "Victor",
                LNAME = "Hempel",
                GDPR = "Ja tak, jeg bekræfter, at jeg vil modtage e-mailmarkedsføring fra Kattegatcentret",
                KONKBET = "Jeg accepterer konkurrencebetingelserne"
            },
            tags = new[] { "SCN 2024" }
        };

        var content = new StringContent(JsonConvert.SerializeObject(memberInfo), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"lists/{listId}/members/", content);

        return response.IsSuccessStatusCode;
    }
}
