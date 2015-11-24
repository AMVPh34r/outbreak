using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {


	public int sceneToStart = 0;				//Index number in build settings of scene to load if changeScenes is true



	void OnTriggerEnter (Collider other)
	{
		// If the colliding gameobject is the player...
		if(other.gameObject == player)
		{
			// ... play the clip at the position of the key...
			AudioSource.PlayClipAtPoint(keyGrab, transform.position);
			

			
			LoadDelayed();
		}
	}



	public void LoadDelayed()
	{
		
		//Load the selected scene, by scene index number in build settings
		Application.LoadLevel (sceneToStart);
	}

}



