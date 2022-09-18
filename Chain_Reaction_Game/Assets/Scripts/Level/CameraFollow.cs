using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private readonly float followSpeed = 5f;
    private readonly float zoomSpeed = 2000f;
    private readonly float minSize = 4.5f;
    private readonly float maxSize = 5f;

    private bool zoomIn = false;
    private Camera cam;

    public Transform target;

    private void Start() {
        cam = GetComponentInChildren<Camera>();
    }

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

        if (Input.GetKeyDown(KeyCode.Space)) {
        }
    }
}
