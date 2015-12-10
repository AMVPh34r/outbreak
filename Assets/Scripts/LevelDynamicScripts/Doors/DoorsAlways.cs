using UnityEngine;
using System.Collections;


public class DoorsAlways : MonoBehaviour {        //this door is always unlocked to the player and opens when they're near
	
	Animator animator;
	bool doorOpen;
	bool doorAlwaysUnlocked = true;        //in essence, this variable is unnecessary if you just removed being unlocked as a criteria

	void Start()
		
	{
		doorOpen = false;
		animator = GetComponent<Animator>();
		
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if (col.gameObject.tag == "Player" && doorAlwaysUnlocked == true) {      //checks if the player is near and if the door is unlocked
			
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