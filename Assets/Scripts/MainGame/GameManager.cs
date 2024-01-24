using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltimateClean;

public class GameManager : MonoBehaviour
{
 
    private int numScenes; // number of scenes in build settings
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;
    [SerializeField] private GameObject statManagerPrefab;
    [SerializeField] private GameObject statDisplayPrefab;
    [SerializeField] private float sceneTransitionDuration = 1.0f;
    [SerializeField] private Color sceneTransitionColor = Color.black;

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

    public void LoadNextScene()
    {
        numScenes = GetScenesTotalAmount();
        int currentSceneIndex = GetCurrentSceneBuildIndex();
        int nextSceneIndex = currentSceneIndex + 1 < numScenes ? currentSceneIndex + 1 : 0; // finds next scene in index. If no more scenes left, load first scene in index
        string nextSceneName = NameOfSceneByBuildIndex(nextSceneIndex);
        PerformTransition(nextSceneName);
    }

    public void LoadSceneByIndex(int buildIndex)
    {
        string sceneName = NameOfSceneByBuildIndex(buildIndex);
        PerformTransition(sceneName);
        // UnityAnalytics.Instance.SendCustomEvent("Left game", false); //this is only called when player leaves the game, and timer runs out.
    }

    public void LoadSceneByIndexWithoutAnalytics(int buildIndex)
    {
        string sceneName = NameOfSceneByBuildIndex(buildIndex);
        PerformTransition(sceneName);
    }


    public static string NameOfSceneByBuildIndex(int buildIndex) // gets name of scene by build index - needed in order to use PerformTransition (from Ultimate Clean GUI Pack)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public void PerformTransition(string scene)
    {
        print("Loading scene: " + scene);
        Transition.LoadLevel(scene, sceneTransitionDuration, sceneTransitionColor);
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

    public int GetScenesTotalAmount()
    {
        if (Application.isEditor) // checks if in editor. Sets numScenes accordingly
        {
            int scenesTotal = SceneManager.sceneCountInBuildSettings;
            return scenesTotal;
        }
        else
        {
            int scenesTotal = SceneManager.sceneCountInBuildSettings;  // get number of scenes in build settings
            return scenesTotal;
        }
    }

    public int GetCurrentSceneBuildIndex()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        return currentBuildIndex;
    }
}
