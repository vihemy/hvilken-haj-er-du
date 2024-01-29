using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class ConfigLoader : Singleton<ConfigLoader>
{
    private Dictionary<string, string> configValues;
    public delegate void ConfigLoadedDelegate();
    public event ConfigLoadedDelegate OnConfigLoaded;

    void Start()
    {
        StartCoroutine(LoadConfig());
    }

    private IEnumerator LoadConfig()
    {
        string configFilePath = Path.Combine(Application.streamingAssetsPath, "config.txt");

        if (configFilePath.Contains("://") || configFilePath.Contains(":///"))
        {
            // Handle platforms where StreamingAssets are in a compressed format
            UnityWebRequest uwr = UnityWebRequest.Get(configFilePath);
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading config file: " + uwr.error);
            }
            else
            {
                // Successfully loaded the config file
                ProcessConfigFile(uwr.downloadHandler.text);
                OnConfigLoaded?.Invoke(); // Invoke the callback
            }
        }
        else
        {
            // Handle Desktop (Windows, macOS) and Editor
            if (File.Exists(configFilePath))
            {
                string dataAsJson = File.ReadAllText(configFilePath);
                ProcessConfigFile(dataAsJson);
                OnConfigLoaded?.Invoke(); // Invoke the callback
            }
            else
            {
                Debug.LogError("Config file not found in StreamingAssets");
            }
        }
    }

    private void ProcessConfigFile(string configFileContent)
    {
        configValues = new Dictionary<string, string>();

        string[] lines = configFileContent.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
            {
                var splitLine = line.Split(new char[] { '=' }, 2);
                if (splitLine.Length == 2)
                {
                    configValues[splitLine[0].Trim()] = splitLine[1].Trim();
                }
            }
        }
    }

    public string LoadFromConfig(string key)
    {
        try
        {
            return configValues[key];
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError($"Key '{key}' not found in config file.");
            return string.Empty;
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError($"Config file not loaded.");
            return string.Empty;
        }
    }
}
