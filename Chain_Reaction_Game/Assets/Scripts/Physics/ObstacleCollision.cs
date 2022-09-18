using UnityEngine;

public class ObstacleCollision : MonoBehaviour {
    private readonly float basicObstacleDamage = 1f;
    private readonly int twiceCollisionCount = 1;
    private readonly float damageAddition = 1f;
    private readonly float damageMinus = -0.5f;

    private GameObject ball;

    public ObstacleEnum obstacleType;
    public int collisionDestroyCounter = 0;
    public float deltaDamage = 0f;

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
            default:
                BasicCollision();
                break;
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
