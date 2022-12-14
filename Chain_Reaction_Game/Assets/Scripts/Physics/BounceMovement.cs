using UnityEngine;

public class BounceMovement : MonoBehaviour {
    private readonly float boostIncreament = 2f;
    private readonly float minSpeed = 1f;
    private readonly Vector2 jumpForce = new(0, 800);
    private readonly float fallThreshold = -3f;
    private readonly float enlargeScale = 1.5f;
    private readonly float heavyDamageMultiplier = 1.5f;
    private readonly float damageMultiply = 2f;
    private readonly float damageDivision = 0.5f;

    private Vector2 lastFrameVelocity;
    private Rigidbody2D rb;
    private float boostCounter = 0;
    private bool boostFlag = false;
    private bool jumpFlag = false;
    private bool heavyFlag = false;
    private float damageMultipler = 1f;
    private GameObject ball;


    public float drag;
    private float boost = 1.2f;
    public float damage;
    public float dragCoe = 0.9f;

    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        ball = Resources.Load("Prefabs/Basic Ball") as GameObject;
    }

    private void FixedUpdate() {
        lastFrameVelocity = rb.velocity;

        // JUMP
        if (jumpFlag) {
            if (rb.velocity.y < fallThreshold) {
                SetJump(false);
            }
        }

        // BOOST
        if (boostFlag) {
            boostCounter -= Time.deltaTime;
            if(boostCounter <= 0) {
                SetBoost(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

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
                Heavy();
                break;
            case ObstacleEnum.TWICE:
                Bounce(collision.contacts[0].normal);
                break;
            case ObstacleEnum.FALL_STRAIGHT:
                FallStraight();
                break;
            case ObstacleEnum.DAMAGE_ADDITION:
                Bounce(collision.contacts[0].normal);
                break;
            case ObstacleEnum.DAMAGE_MINUS:
                Bounce(collision.contacts[0].normal);
                break;
            case ObstacleEnum.DAMAGE_MULTIPLY:
                Bounce(collision.contacts[0].normal);
                DamageMultiply();
                break;
            case ObstacleEnum.DAMAGE_DIVISION:
                Bounce(collision.contacts[0].normal);
                DamageDivision();
                break;
            default:
                Bounce(collision.contacts[0].normal);
                break;
        }
    }

    public void AddDamage(float d) {
        d *= damageMultipler;
        damage += d;
    }

    private void DamageMultiply() {
        damage *= damageMultiply;
    }

    private void DamageDivision() {
        damage *= damageDivision;
    }

    private void Bounce(Vector2 collisionNormal) {
        if (heavyFlag) return;
        float speed = lastFrameVelocity.magnitude * drag;
        drag = boostFlag ? 1f : dragCoe;
        if (speed < minSpeed) return;
        Vector2 direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = speed * direction;
    }

    private void FallStraight() {
        rb.velocity = new(0, 0);
    }

    private void Heavy() {
        heavyFlag = true;
        damageMultipler *= heavyDamageMultiplier;
    }

    private void Enlarge() {
        gameObject.transform.localScale *= enlargeScale;
    }

    private void Bouncy() {
        dragCoe = 1f;
    }

    private void Duplicate() {
        GameObject go = Instantiate(ball, gameObject.transform.position, Quaternion.identity);
        LevelData.activeBalls++;
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-800, 800), Random.Range(-800, 800)));
    }

    private void SetBoost(bool enable) {
        if (enable) {
            boostFlag = true;
            boostCounter = boostIncreament;
            SetDrag(false);
            SetGravity(false);
        } else {
            boostFlag = false;
            boostCounter = 0;
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
