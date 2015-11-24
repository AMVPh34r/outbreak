using UnityEngine;
using System.Collections;

public class CameraFollowController : MonoBehaviour {
    public Transform target;
    public float speed = 5f;    	// Camera follow speed

    private Vector3 offset;         // Initial camera offset


    void Start() {
        this.offset = transform.position - target.position;
    }


    void FixedUpdate() {
        Vector3 camPos = target.position + this.offset;
        transform.position = Vector3.Lerp(transform.position, camPos, speed * Time.deltaTime);
    }
}
