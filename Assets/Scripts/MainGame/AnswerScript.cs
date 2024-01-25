using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswerScript : MonoBehaviour
{
    [HideInInspector] public int answerBin;
    private AudioManager audioManager;
    private ScoreManager scoreManager;
    private GameManager gameManager;
    private Slider slider;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Answer(string answer)
    {
        if (answer.Contains(", ")) // if pointBinCorrespondent includes multiple answers (and therefore includes ",")... 
        {
            string[] words = answer.Split(", "); // split pointBinCorrespondents into several words

            foreach (var word in words) // add point to pointBin corresponding to each word
            {
                scoreManager.pointBins[word] += 1;
            }
        }
        else
        {
            scoreManager.pointBins[answer] += 1; // ... add 1 to the proper pointBin in scoreManager.
        }

        SceneLoader.Instance.LoadNextScene(); // calls nextQuestion-method in gameManager
    }


    public void SliderAnswer()
    {
        slider = FindObjectOfType<Slider>(); // finds slider and extracts value from slider.value
        float value = slider.value;

        switch (value)
        {
            case 0:
                scoreManager.pointBins["Smaaplettet roedhaj"] += 1;
                break;
            case float n when (n <= 2): // when value (here named n) has sepcifik conditional relation to number = assign points to corresponding bin.
                scoreManager.pointBins["Sandtigerhaj"] += 1;
                break;
            case float n when (n >= 3 && n <= 9):
                scoreManager.pointBins["Groenlandshaj"] += 1;
                break;
            case float n when (n >= 10):
                scoreManager.pointBins["Wobbegong"] += 1;
                scoreManager.pointBins["Hvalhaj"] += 1;
                break;
            default:
                break;
        }

        SceneLoader.Instance.LoadNextScene(); // calls nextQuestion-method in gameManager
    }
}