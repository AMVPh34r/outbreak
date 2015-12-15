using UnityEngine;
using System.Collections;


public class Doors : MonoBehaviour {
	
	Animator animator;
	bool doorOpen;
	bool doorUnlocked = false;    //door starts out locked
	
	void Start()
		
	{
		doorOpen = false;
		animator = GetComponent<Animator>();
		
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && PlayerMovement.keyCount >= 1 && doorUnlocked == false) {

			doorUnlocked = true;
			PlayerMovement.keyCount = (PlayerMovement.keyCount-1);  //uses up one key to unlock the door
		}

		if (col.gameObject.tag == "Player" && doorUnlocked == true) {
			
			doorOpen = true;
			DoorsControl("Open");
		}
		
	}
	
	void OnTriggerExit(Collider col)
	{
		
		if(doorOpen)
		{
			doorOpen = false;
			DoorsControl("Close");
			
		}
	}
	
	void DoorsControl(string direction)
		
	{
		animator.SetTrigger(direction);
		
	}
	
}