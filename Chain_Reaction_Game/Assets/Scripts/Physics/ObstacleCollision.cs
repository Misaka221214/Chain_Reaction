using UnityEngine;

public class ObstacleCollision : MonoBehaviour {
    public int collisionDestroyCounter = 0;

    private readonly int twiceCollisionCount = 1;

    public ObstacleEnum obstacleType;

    private void OnCollisionEnter2D(Collision2D collision) {
        switch (obstacleType) {
            case ObstacleEnum.BASIC:
                BasicCollision();
                break;
            case ObstacleEnum.BOOST:
                BasicCollision();
                break;
            case ObstacleEnum.JUMP:
                BasicCollision();
                break;
            case ObstacleEnum.DUPLICATE:
                BasicCollision();
                break;
            case ObstacleEnum.ENLARGE:
                BasicCollision();
                break;
            case ObstacleEnum.BOUNCY:
                BasicCollision();
                break;
            case ObstacleEnum.HEAVY:
                BasicCollision();
                break;
            case ObstacleEnum.TWICE:
                TwiceCollision();
                break;
            case ObstacleEnum.FALL_STRAIGHT:
                BasicCollision();
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

    private void TwiceCollision() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject go in gos) {
            go.GetComponentInChildren<ObstacleCollision>().collisionDestroyCounter = twiceCollisionCount;
        }

        Destroy(gameObject);
    }
}
