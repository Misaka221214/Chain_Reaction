using UnityEngine;
using UnityEngine.SceneManagement;

//LevelProgressionManager is a singleton
public class LevelProgressionManager : MonoBehaviour {
    public static LevelProgressionManager Instance { get; private set; }

    //public System.Action OnLevelPassed;
    public int curLevelBuildIndex;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
            ResetLevelProgress();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void HandleOnLevelPassed() {
        Debug.Log("Should load next scene");

        Scene curScene = SceneManager.GetActiveScene();
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextLevelIndex = curScene.buildIndex + 1;

        // Load the buff selection scene unless this is the last level

        if (nextLevelIndex < sceneCount - 3) //Hardcoded, -2 because of the Win Scene, Lose Scene and BuffSelection Scene
        {
            curLevelBuildIndex = nextLevelIndex;
            SceneManager.LoadScene("BuffSelection");
        }
        else
        {
            SceneManager.LoadScene("Win");
            LevelData.ResetLevelData();
        }

        //if (nextSceneIndex < sceneCount - 2) //Hardcoded, -2 because of the Win Scene and Lose Scene
        //{
        //    curLevelBuildIndex = nextSceneIndex;
        //    SceneManager.LoadScene(curLevelBuildIndex);
        //} else {
        //    SceneManager.LoadScene("Win");
        //    LevelData.ResetLevelData();
        //}
    }

    public void HandleFinishBuffSelection()
    {
        SceneManager.LoadScene(curLevelBuildIndex);
    }

    public void LoadLoseScreen() {
        SceneManager.LoadScene("Lose");
        LevelData.ResetLevelData();
    }

    public void ReloadCurrentScreen() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelData.ResetLevelData();
    }

    public void ResetLevelProgress() {
        curLevelBuildIndex = 1;
        LevelData.ResetLevelData();
    }
}
