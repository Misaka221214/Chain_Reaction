using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {
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
            default:
                BasicCollision();
                break;
        }
    }

    private void BasicCollision() {
        Destroy(gameObject);
    }
}
