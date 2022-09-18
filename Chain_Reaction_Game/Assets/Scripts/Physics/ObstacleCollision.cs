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
            default:
                break;
        }
    }

    private void BasicCollision() {
        Destroy(gameObject);
    }
}
