using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TimeOut : MonoBehaviour
{
    [SerializeField] private float timeToReset;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        Invoke("LoadFirstScene", timeToReset); // Invokes timedown-method when scene is loaded (new gamemanager for each scene)
    }
    private void Update()
    {
        //load first scene if this is not 1. scene & if there have been no touch-input for x seconds
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.touchCount != 0)
        {
            CancelInvoke("LoadFirstScene"); // prior invoke is cancelled. Otherwise, starts invokes every second
            Invoke("LoadFirstScene", timeToReset); // new invoke is set
        }
    }

    private void LoadFirstScene()
    {
        gameManager.LoadSceneByIndex(0);
    }
}
