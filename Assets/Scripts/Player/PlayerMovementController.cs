using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MonoBehaviour {
    public float speed = 6f;    // Player's movement speed

    private Vector3 movement;           // Player's movement direction
    private Animator anim;              // Reference to the animator component.
    private Rigidbody playerRigidbody;  // Reference to the player's rigidbody.
    private int floorMask;              // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    public float camRayLength = 100f;  // The length of the ray from the camera into the scene.
	public static int keyCount;

    void Awake()  {
        // Create a layer mask for the floor layer.
        this.floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        this.anim = GetComponent<Animator>();
        this.playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        // Store the input axes.
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v);
    }

    void Move(float h, float v) {
        // Set the movement vector based on the axis input.
        this.movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        this.movement = this.movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        this.playerRigidbody.MovePosition(transform.position + this.movement);

		//transform.rotation=Quaternion.LookRotation (movement.normalized);
		if (h != 0 || v != 0) {
			transform.rotation = Quaternion.LookRotation (movement);
		}

    }

    void Turning() {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, this.camRayLength, this.floorMask)) {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            //Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            //this.playerRigidbody.MoveRotation(newRotatation);
        }
    }

    void Animating(float h, float v) {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        this.anim.SetBool("PlayerIsMoving", walking);
    }



	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.name == "Key")
		{
			
			PlayerMovementController.keyCount += 1;
			
			collider.gameObject.SetActive (false);

		}
		
	}
}