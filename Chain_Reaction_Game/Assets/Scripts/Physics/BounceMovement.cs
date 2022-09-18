using UnityEngine;

public class BounceMovement : MonoBehaviour {
    private Vector2 lastFrameVelocity;
    private Rigidbody2D rb;

    public float drag = 10f;

    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector2 collisionNormal) {
        float speed = lastFrameVelocity.magnitude * drag;
        Vector2 direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = speed * direction;
    }
}
