using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    private string input;
    public TextMeshProUGUI finalScoreText;
    [SerializeField] private AudioManager audioManager;


    public void OpenGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        audioManager.Stop("Timer"); // stops timer-sfx
        audioManager.Play("Success"); // Plays success-sfx
    }
    
    public void LoadResultScene()   //
    {
        SceneManager.LoadScene(1);
    }

  
}
