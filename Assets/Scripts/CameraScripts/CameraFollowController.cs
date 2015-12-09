using UnityEngine;
using System.Collections;

public class CameraFollowController : MonoBehaviour {
    public Transform target;
    public float speed = 5f;    	// Camera follow speed

    private Vector3 offset;         // Initial camera offset
	private float fov;
	private float minFov = 15f;
	private float maxFov = 90f;
	private float sensitivity = 1f;


    void Start() {
        this.offset = transform.position - target.position;
    }


    void FixedUpdate() {
        Vector3 camPos = target.position + this.offset;
        transform.position = Vector3.Lerp(transform.position, camPos, speed * Time.deltaTime);

		// Zoom camera when scroll wheel is spun
		fov = Camera.main.fieldOfView;
		fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
    }
}
