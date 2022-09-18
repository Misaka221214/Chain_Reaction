using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//LevelProgressionManager is a singleton
public class LevelProgressionManager : MonoBehaviour
{
    public static LevelProgressionManager Instance { get; private set; }

    //public System.Action OnLevelPassed;
    public int curLevelBuildIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            ResetLevelProgress();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleOnLevelPassed()
    {
        Debug.Log("Should load next scene");

        Scene curScene = SceneManager.GetActiveScene();
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = curScene.buildIndex + 1;
        if (nextSceneIndex < sceneCount - 2) //Haedcoded, -2 because of the Win Scene and Lose Scene
        {
            curLevelBuildIndex = nextSceneIndex;
            SceneManager.LoadScene(curLevelBuildIndex);
        } else
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void ResetLevelProgress()
    {
        curLevelBuildIndex = 1;
    }
}
