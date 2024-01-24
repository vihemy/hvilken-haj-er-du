using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSharer : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI resultText;

    private void Start()
    {
        ShareResult();
    }

    void ShareResult()
    {
        //resultText.text = scoreManager.pointBins;
    }
}
