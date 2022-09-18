using UnityEngine;

public class ObstacleCollision : MonoBehaviour {
    private readonly float basicObstacleDamage = 1f;
    private readonly int twiceCollisionCount = 1;
    private readonly float damageAddition = 1f;
    private readonly float damageMinus = -0.5f;
    private readonly int specialObstacleThreshold = 10;
    private readonly float level1BossMaxHealth = 100f;
    private readonly float level2BossMaxHealth = 120f;
    private readonly float level3BossMaxHealth = 150f;

    private GameObject ball;
    private float level1BossHealth = 100f;
    private float level2BossHealth = 120f;
    private float level3BossHealth = 150f;

    public ObstacleEnum obstacleType;
    public int collisionDestroyCounter = 0;
    public float deltaDamage = 0f;

    private void Start() {
        if (obstacleType == ObstacleEnum.RANDOM) {
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
            case ObstacleEnum.LEVEL_2_BOSS:
                Level2BossCollision();
                break;
            case ObstacleEnum.LEVEL_3_BOSS:
                Level3BossCollision();
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
        if (gamePlayManagerObject && LevelData.activeBalls <= 0) {
            LevelGamePlayManager levelGamePlayManager = gamePlayManagerObject.GetComponentInChildren<LevelGamePlayManager>();
            if (levelGamePlayManager) {
                levelGamePlayManager.InstantiateBall();
            }
        }
    }

    private void Level2BossCollision() {
        BounceMovement bm = ball.GetComponentInChildren<BounceMovement>();
        if (bm) {
            level2BossHealth -= bm.damage;
            LevelData.activeBalls--;
            Destroy(ball);
        }

        if (level2BossHealth <= 0) {
            LevelData.level2BossIsDead = true;
            // TODO: Next screen
            LevelProgressionManager.Instance.HandleOnLevelPassed();
            return;
        }

        GameObject gamePlayManagerObject = GameObject.Find("LevelGamePlayManager");
        if (gamePlayManagerObject && LevelData.activeBalls <= 0) {
            LevelGamePlayManager levelGamePlayManager = gamePlayManagerObject.GetComponentInChildren<LevelGamePlayManager>();
            if (levelGamePlayManager) {
                levelGamePlayManager.InstantiateBall();
            }
        }
    }

    private void Level3BossCollision() {
        BounceMovement bm = ball.GetComponentInChildren<BounceMovement>();
        if (bm) {
            level3BossHealth -= bm.damage;
            LevelData.activeBalls--;
            Destroy(ball);
        }

        if (level3BossHealth <= 0) {
            LevelData.level3BossIsDead = true;
            // TODO: Next screen
            LevelProgressionManager.Instance.HandleOnLevelPassed();
            return;
        }

        GameObject gamePlayManagerObject = GameObject.Find("LevelGamePlayManager");
        if (gamePlayManagerObject && LevelData.activeBalls <= 0) {
            LevelGamePlayManager levelGamePlayManager = gamePlayManagerObject.GetComponentInChildren<LevelGamePlayManager>();
            if (levelGamePlayManager) {
                levelGamePlayManager.InstantiateBall();
            }
        }
    }

    public float GetBossHealthPercentage(LevelEnum level) {
        return level switch {
            LevelEnum.LEVEL_1 => level1BossHealth / level1BossMaxHealth,
            LevelEnum.LEVEL_2 => level2BossHealth / level2BossMaxHealth,
            LevelEnum.LEVEL_3 => level3BossHealth / level3BossMaxHealth,
            _ => 0f,
        };
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
