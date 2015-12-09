using UnityEngine;
using System.Collections;


public class Doors : MonoBehaviour {
	
	Animator animator;
	bool doorOpen;
	
	void Start()
		
	{
		doorOpen = false;
		animator = GetComponent<Animator>();
		
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if (col.gameObject.tag == "Player" && PlayerMovement.keyCount >=1) {
			
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