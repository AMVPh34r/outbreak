using UnityEngine;
using System.Collections;

public class KeyGate : MonoBehaviour {
	
	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.name == "Player" && PlayerMovement.keyCount ==1)
		{

			
			Destroy (gameObject);
			
		}
		
	}
}
