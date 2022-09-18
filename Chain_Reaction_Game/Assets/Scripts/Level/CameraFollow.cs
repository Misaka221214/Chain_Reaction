using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private readonly float followSpeed = 5f;

    public Transform target;

    void Update() {
        if (target) {
            Vector3 newPos = new(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        } else {
            transform.position = Vector3.Slerp(transform.position, new Vector3(0, 0, -10f), followSpeed * Time.deltaTime);
            GameObject go = GameObject.FindGameObjectWithTag("Ball");
            if (go) {
                target = go.transform;
            }
        }
    }
}
