using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
    string myLog = "*begin log";
    string filename = "";
    bool doShow = false;
    int kChars = 700;
    Vector2 scrollPosition;
    [Header("Key to trigger on screen console:")]
    [SerializeField] private KeyCode keyCode = KeyCode.LeftControl;
    [SerializeField] private Color backgroundColor = new Color(0, 0, 0, 0.7f);  // Semi-transparent black

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            doShow = !doShow;
        }

        if (Input.touchCount == 10)
        {
            doShow = !doShow;
        }
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        // Append new log to existing logs
        myLog = myLog + "\n" + logString;
        if (type == LogType.Exception)
        {
            myLog = myLog + "\n" + stackTrace;  // Append stack trace for exceptions
        }
        if (myLog.Length > kChars)
        {
            myLog = myLog.Substring(myLog.Length - kChars);
        }

        // Write to file (if needed)
        if (filename == "")
        {
            string d = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            System.IO.Directory.CreateDirectory(d);
            string r = Random.Range(1000, 9999).ToString();
            filename = d + "/log-" + r + ".txt";
        }
        try
        {
            System.IO.File.AppendAllText(filename, logString + "\n");
        }
        catch { }
    }

    void OnGUI()
    {
        if (!doShow) return;

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));

        // Draw a background for the text area
        GUI.backgroundColor = backgroundColor;
        GUI.Box(new Rect(10, 10, 540, 370), GUIContent.none);

        // Use a ScrollView to display log messages
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(540), GUILayout.Height(370));
        GUILayout.Label(myLog);
        GUILayout.EndScrollView();
    }
}
