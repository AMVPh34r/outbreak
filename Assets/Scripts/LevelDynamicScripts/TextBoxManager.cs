using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;
	public TextAsset textFile;
	public string[] textLines;
	public Text textTitle;
	public Text theText;
	public int currentLine;
	public int endAtLine;
	public PlayerMovement player;
	
	
	void Start() {
		player = FindObjectOfType<PlayerMovement> ();

		if (textFile != null) {
			textBox.SetActive (true);
			textLines = textFile.text.Split ('\n');
			textTitle = GameObject.Find ("Canvas/Panel/Title").gameObject.GetComponent<Text>();
			theText = GameObject.Find ("Canvas/Panel/Text").gameObject.GetComponent<Text>();
		} else {
			textBox.SetActive (false);
		}

		if(endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}
	}



	void Update()
	{
		if (textBox.activeSelf) {
			string[] line = textLines[currentLine].Split(new string[] { "::" }, System.StringSplitOptions.None);

			textTitle.text = line[0];
			theText.text = line[1];

			if (Input.GetKeyDown (KeyCode.Return)) {
				currentLine += 1;
			}
		}

		if (currentLine > endAtLine) {
			textBox.SetActive (false);
		}
	}

	
}
