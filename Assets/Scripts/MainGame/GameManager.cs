using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltimateClean;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;
    [SerializeField] private GameObject statManagerPrefab;
    [SerializeField] private GameObject statDisplayPrefab;

    private void Awake()
    {
        //CheckManagerInstances("GameManager", gameManagerPrefab); // check if there are multiple instances of GameManager
        CheckPrefabInstances("ScoreManager", scoreManagerPrefab); // check if there are multiple instances of ScoreManager
        CheckPrefabInstances("StatManager", statManagerPrefab); // check if there are multiple instances of StatManager
        CheckPrefabInstances("StatDisplay", statDisplayPrefab);
        RemoveCurser();
    }

    private void RemoveCurser()
    {
        Cursor.visible = false;
    }


    private void CheckPrefabInstances(string tag, GameObject prefabToInstantiate)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > 1) // destroy all but first instance
        {
            for (int i = 0; i < objs.Length; i++)// loop through instances
            {
                if (i > 0)
                {
                    Destroy(objs[i]); // Dont have multiple instances
                }
            }

        }
        if (objs.Length == 1) // if not multiple instances: dont destroy the single instance on load
        {
            DontDestroyOnLoad(objs[0]);
        }
        if (objs.Length < 1) // if no instances: instantiate
        {
            Instantiate(prefabToInstantiate);
        }
    }
}
