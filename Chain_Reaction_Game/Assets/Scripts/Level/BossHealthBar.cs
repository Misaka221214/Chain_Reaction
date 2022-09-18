using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    private float healthPercentage;
    private Vector3 initialPosition;

    public ObstacleCollision boss;
    public LevelEnum level;
    public SpriteRenderer renderer;

    private void Start() {
        initialPosition = transform.position;
    }

    private void FixedUpdate() {
        healthPercentage = boss.GetBossHealthPercentage(level);
        transform.position = new Vector3(initialPosition.x - (1f - healthPercentage) * renderer.bounds.size.x, initialPosition.y, initialPosition.z);
    }
}
