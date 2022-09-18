using UnityEngine;

public class BounceMovement : MonoBehaviour {
    private Vector2 lastFrameVelocity;
    private Rigidbody2D rb;
    private int boostCollisionCount = 0;
    private bool boostFlag = false;
    private bool jumpFlag = false;
    private float dragCoe = 0.7f;
    private bool fallThroughFlag = false;

    private readonly int boostIncreament = 5;
    private readonly float minSpeed = 1f;
    private readonly Vector2 jumpForce = new(0, 500);
    private readonly float fallThreshold = -3f;
    private readonly float enlargeScale = 1.5f;

    public float drag;
    public float boost;

    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        lastFrameVelocity = rb.velocity;

        // JUMP
        if (jumpFlag) {
            if (rb.velocity.y < fallThreshold) {
                SetJump(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // BOOST
        if (boostFlag) {
            boostCollisionCount--;
            if (boostCollisionCount <= 0) {
                SetBoost(false);
            }
        }

        ObstacleCollision obstacle = collision.collider.GetComponentInChildren<ObstacleCollision>();
        ObstacleEnum obstacleEnum = ObstacleEnum.BASIC;
        if (obstacle) {
            obstacleEnum = obstacle.obstacleType;
        }

        switch (obstacleEnum) {
            case ObstacleEnum.BASIC:
                Bounce(collision.contacts[0].normal);
                break;
            case ObstacleEnum.BOOST:
                SetBoost(true);
                Bounce(collision.contacts[0].normal);
                break;
            case ObstacleEnum.JUMP:
                Bounce(collision.contacts[0].normal);
                SetJump(true);
                break;
            case ObstacleEnum.DUPLICATE:
                Bounce(collision.contacts[0].normal);
                Duplicate();
                break;
            case ObstacleEnum.ENLARGE:
                Bounce(collision.contacts[0].normal);
                Enlarge();
                break;
            case ObstacleEnum.BOUNCY:
                Bounce(collision.contacts[0].normal);
                Bouncy();
                break;
            case ObstacleEnum.HEAVY:
                FallThrough();
                break;
            default:
                Bounce(collision.contacts[0].normal);
                break;
        }
    }

    private void Bounce(Vector2 collisionNormal) {
        if (fallThroughFlag) return;
        float speed = lastFrameVelocity.magnitude * drag;
        drag = boostFlag ? 1f : dragCoe;
        if (speed < minSpeed) return;
        Vector2 direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = speed * direction;
    }

    private void FallThrough() {
        fallThroughFlag = true;
    }

    private void Enlarge() {
        gameObject.transform.localScale *= enlargeScale;
    }

    private void Bouncy() {
        dragCoe = 1f;
    }

    private void Duplicate() {
        GameObject go = Instantiate(gameObject);
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), Random.Range(-200, 200)));
    }

    private void SetBoost(bool enable) {
        if (enable) {
            boostFlag = true;
            boostCollisionCount = boostIncreament;
            SetDrag(false);
            SetGravity(false);
        } else {
            boostFlag = false;
            boostCollisionCount = 0;
            SetDrag(true);
            SetGravity(true);
        }
    }

    private void SetJump(bool enable) {
        if (enable) {
            jumpFlag = true;
            ApplyForce(jumpForce);
            SetIgnoreObstacleCollision(true);
        } else {
            jumpFlag = false;
            SetIgnoreObstacleCollision(false);
        }
    }

    private void SetIgnoreObstacleCollision(bool enable) {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");

        if (enable) {
            foreach (GameObject go in gos) {
                Physics2D.IgnoreCollision(gameObject.GetComponentInChildren<Collider2D>(), go.GetComponentInChildren<Collider2D>(), true);
            }
        } else {
            foreach (GameObject go in gos) {
                Physics2D.IgnoreCollision(gameObject.GetComponentInChildren<Collider2D>(), go.GetComponentInChildren<Collider2D>(), false);
            }
        }
    }

    private void ApplyForce(Vector2 force) {
        rb.AddForce(force);
    }

    private void SetDrag(bool enable) {
        if (enable) {
            drag = dragCoe;
        } else {
            drag = boost;
        }
    }

    private void SetGravity(bool enable) {
        if (enable) {
            rb.gravityScale = 1f;
        } else {
            rb.gravityScale = 0f;
        }
    }
}
