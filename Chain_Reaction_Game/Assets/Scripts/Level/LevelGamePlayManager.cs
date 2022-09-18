using UnityEngine;

public class LevelGamePlayManager : MonoBehaviour {
    private GameObject ball;
    private bool launchBall = false;
    private bool draggingBall = false;

    public LevelEnum level;
    public GameObject ClickAndDragText;

    // Ball Launcher fields
    Rigidbody2D rb = null;
    readonly float power = 10f;
    Vector2 minPower = new(-8, -8);
    Vector2 maxPower = new(8, 8);
    Vector3 startPoint;
    Vector3 endPoint;
    Camera cam;
    GameObject go;
    Vector2 force;


    void Start() {
        ball = Resources.Load("Prefabs/Basic Ball") as GameObject;
        cam = Camera.main;
        minPower = new(-8, -8);
        maxPower = new(8, 8);
        InstantiateBall();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            LevelProgressionManager.Instance.ReloadCurrentScreen();
        }
        if (launchBall) {
            LaunchBall();
        }
    }

    public void InstantiateBall() {
        switch (level) {
            case LevelEnum.LEVEL_1:
                LevelData.level1RemainingBalls--;
                if (LevelData.level1RemainingBalls < 0 && !LevelData.level1BossIsDead) {
                    LevelProgressionManager.Instance.LoadLoseScreen();
                    return;
                }
                launchBall = true;
                break;
            case LevelEnum.LEVEL_2:
                LevelData.level2RemainingBalls--;
                if (LevelData.level2RemainingBalls < 0 && !LevelData.level2BossIsDead) {
                    LevelProgressionManager.Instance.LoadLoseScreen();
                    return;
                }
                launchBall = true;
                break;
            case LevelEnum.LEVEL_3:
                LevelData.level3RemainingBalls--;
                if (LevelData.level3RemainingBalls < 0 && !LevelData.level3BossIsDead) {
                    LevelProgressionManager.Instance.LoadLoseScreen();
                    return;
                }
                launchBall = true;
                break;
            default:
                break;
        }
    }

    private void LaunchBall() {
        ClickAndDragText.SetActive(true);
        if (Input.GetMouseButtonDown(0)) {
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0;
            if (!draggingBall) {
                go = Instantiate(ball, startPoint, Quaternion.identity);
                LevelData.activeBalls++;
                rb = go.GetComponent<Rigidbody2D>();
                rb.gravityScale = 0f;
                draggingBall = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            draggingBall = false;
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;

            force = new(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            if (rb) {
                rb.AddForce(force * power, ForceMode2D.Impulse);
                rb.gravityScale = 1f;
                launchBall = false;
                ClickAndDragText.SetActive(false);
            }
        }
    }
}
