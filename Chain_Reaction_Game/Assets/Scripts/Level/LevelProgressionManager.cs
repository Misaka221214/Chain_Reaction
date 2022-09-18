using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//LevelProgressionManager is a singleton
public class LevelProgressionManager : MonoBehaviour
{
    public static LevelProgressionManager Instance { get; private set; }

    //public System.Action OnLevelPassed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
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
        if (curScene.buildIndex < sceneCount - 2) //Haedcoded, -2 because of the Win Scene and Lose Scene
        {
            SceneManager.LoadScene(curScene.buildIndex + 1);
        } else
        {
            SceneManager.LoadScene("Win");
        }
    }
}
