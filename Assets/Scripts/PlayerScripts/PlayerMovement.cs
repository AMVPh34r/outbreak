using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float turnSmoothing = 15f;	// A smoothing value for turning the player.
	public float speedDampTime = 0.1f;	// The damping for the speed parameter
	public static int keyCount;			// The number of keys in the player's posession

	private Animator anim;				// Reference to the animator component.
	private HashIDs hash;				// Reference to the HashIDs.
	
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
	}
	
	
	void FixedUpdate ()
	{
		// Cache the inputs.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool sneak = Input.GetButton("Sneak");
		
		MovementManagement(h, v, sneak);
	}
	
	
	void Update ()
	{
		AudioManagement ();
	}
	
	
	void MovementManagement (float horizontal, float vertical, bool sneaking)
	{
		// Set the sneaking parameter to the sneak input.
		anim.SetBool(hash.sneakingBool, sneaking);
		
		// If there is some axis input...
		if (horizontal != 0f || vertical != 0f) {
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating (horizontal, vertical);
			anim.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
			anim.SetBool (hash.playerIsMovingBool, true);
		} else {
			// Otherwise set the speed parameter to 0.
			anim.SetFloat (hash.speedFloat, 0f);
			anim.SetBool (hash.playerIsMovingBool, false);
		}
	}
	
	
	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		GetComponent<Rigidbody>().MoveRotation(newRotation);
	}

	void AudioManagement()
	{
		// If the player is currently moving and footstep audio is not playing, play it
		if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
		{
			if(!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
		}
		else
			// Otherwise stop footstep audio
			GetComponent<AudioSource>().Stop();
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.tag == Tags.key) {
			PlayerMovement.keyCount += 1;

			Debug.Log (keyCount);
			collider.gameObject.SetActive (false);
		}
	}
}
