using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;

	public TextAsset textFile;
	public string[] textLines;

	public Text theText;
	// Use this for initialization
	public int currentLine;
	public int endAtLine;

	public PlayerMovement player;
	
	
	void Start() {
		player = FindObjectOfType<PlayerMovement> ();

		if(textFile !=  null)
			
			textLines = (textFile.text.Split('\n'));

		if(endAtLine == 0)
			
		{
			endAtLine = textLines.Length - 1;
		}
	}



	void Update()
	{
		if (textBox.activeSelf) {
			theText.text = textLines [currentLine];

			if (Input.GetKeyDown (KeyCode.Return)) {
				currentLine += 1;
			}
		}

		if (currentLine > endAtLine) {
			textBox.SetActive (false);
		}
	}

	
}
