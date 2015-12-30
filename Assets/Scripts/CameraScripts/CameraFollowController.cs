using UnityEngine;
using System.Collections;

public class CameraFollowController : MonoBehaviour {
    public Transform target;
    public float speed = 5f;    		// Camera follow speed
	public float minFov = 15f;			// Minimum camera FOV
	public float maxFov = 76f;			// Maximum camera FOV
	public float zoomSensitivity = 10f;	// Sensitivity of scroll wheel for zooming

    private Vector3 offset;         	// Initial camera offset
	private float fovTarget;			// Target FOV when zooming the camera

    void Start() {
        this.offset = transform.position - target.position;
    }

    void FixedUpdate() {
        Vector3 camPos = target.position + this.offset;
        transform.position = Vector3.Lerp(transform.position, camPos, speed * Time.deltaTime);

		// Zoom camera when scroll wheel is spun
		fovTarget = Camera.main.fieldOfView + Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
		fovTarget = Mathf.Clamp(fovTarget, minFov, maxFov);

		if (GlobalVars.controlsEnabled == true) {
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, fovTarget, Time.deltaTime);
		}
    }
}
