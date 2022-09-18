using UnityEngine;

public class ObstacleCollision : MonoBehaviour {
    private readonly float basicObstacleDamage = 1f;
    private readonly int twiceCollisionCount = 1;
    private readonly float damageAddition = 1f;
    private readonly float damageMinus = -0.5f;
    private readonly int specialObstacleThreshold = 5;
    //private readonly float level1BossMaxHealth = 20f;

    private GameObject ball;
    private float level1BossHealth = 20f;

    public ObstacleEnum obstacleType;
    public int collisionDestroyCounter = 0;
    public float deltaDamage = 0f;

    private void Start() {
        if(obstacleType == ObstacleEnum.RANDOM) {
            int chance = Random.Range(0, 100);
            GameObject prefab = Resources.Load(PrefabDict.prefabDict[ObstacleEnum.BASIC]) as GameObject;
            if (chance < specialObstacleThreshold) {
                int index = Random.Range(0, LevelData.playerObstacleList.Count);
                prefab = Resources.Load(PrefabDict.prefabDict[LevelData.playerObstacleList[index]]) as GameObject;
            }
            Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        ball = collision.collider.gameObject;
        switch (obstacleType) {
            case ObstacleEnum.BASIC:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.BOOST:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.JUMP:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.DUPLICATE:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.ENLARGE:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.BOUNCY:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.HEAVY:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.TWICE:
                TwiceCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.FALL_STRAIGHT:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.DAMAGE_ADDITION:
                ChangeDamageToObstacles(damageAddition);
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.DAMAGE_MINUS:
                ChangeDamageToObstacles(damageMinus);
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.DAMAGE_MULTIPLY:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.DAMAGE_DIVISION:
                BasicCollision();
                AddDamageToBall(basicObstacleDamage);
                break;
            case ObstacleEnum.LEVEL_1_BOSS:
                Level1BossCollision();
                break;
            default:
                BasicCollision();
                break;
        }
    }

    private void Level1BossCollision() {
        BounceMovement bm = ball.GetComponentInChildren<BounceMovement>();
        if (bm) {
            level1BossHealth -= bm.damage;
            LevelData.activeBalls--;
            Destroy(ball);
        }

        if (level1BossHealth <= 0) {
            LevelData.level1BossIsDead = true;
            // TODO: Next screen
            LevelProgressionManager.Instance.HandleOnLevelPassed();
            return;
        }

        GameObject gamePlayManagerObject = GameObject.Find("LevelGamePlayManager");
        if (gamePlayManagerObject) {
            LevelGamePlayManager levelGamePlayManager = gamePlayManagerObject.GetComponentInChildren<LevelGamePlayManager>();
            if (levelGamePlayManager) {
                levelGamePlayManager.InstantiateBall();
            }
        }
    }

    private void BasicCollision() {
        if (collisionDestroyCounter > 0) {
            collisionDestroyCounter--;
            return;
        }

        Destroy(gameObject);
    }

    private void ChangeDamageToObstacles(float damageChange) {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject go in gos) {
            go.GetComponentInChildren<ObstacleCollision>().deltaDamage += damageChange;
        }
    }

    private void AddDamageToBall(float damage) {
        if (ball) {
            BounceMovement bm = ball.GetComponentInChildren<BounceMovement>();
            if (bm) {
                bm.AddDamage(damage + deltaDamage);
            }
        }
    }

    private void TwiceCollision() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject go in gos) {
            go.GetComponentInChildren<ObstacleCollision>().collisionDestroyCounter = twiceCollisionCount;
        }

        Destroy(gameObject);
    }
}
