using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImage : MonoBehaviour
{
    // ChooseRandomSprite variables
    public Component imageComponent;
    public Sprite[] spritesToSpawn;
    private int randomIndex;
    private Sprite chosenImage;

    // InvokeRepeating variables
    public float interval;

    void ChooseRandomSprite()
    {
        randomIndex = Random.Range(0, spritesToSpawn.Length);
        chosenImage = spritesToSpawn[randomIndex];
        gameObject.GetComponent<Image>().sprite = chosenImage;
    }

    //void Answers()
    //{
          
    //    if(Input.GetKeyDown(KeyCode.Alpha1) && randomIndex == 1 ||  Input.GetKeyDown(KeyCode.Alpha2) && randomIndex == 2 || Input.GetKeyDown(KeyCode.Alpha3) && randomIndex == 3)
    //    {
    //        Debug.Log("Correct!");
    //    }

    //}
    void Start()
    {
     // Starts repition of function called
        InvokeRepeating("ChooseRandomSprite", 0.0f, interval);
        InvokeRepeating("Answers", 0.0f, interval);
    }

    private void Update()
    {
    }
}
