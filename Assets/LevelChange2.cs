using UnityEngine;
using System.Collections;

public class LevelChange2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider others)
	{	
		if(others.gameObject.name == "OutbreakPlayerScientist")
		{
			Application.LoadLevel("Level3");
		}
	}
}
