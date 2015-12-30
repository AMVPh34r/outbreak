using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour {
	public TextAsset textFile;
	public GameObject textBox;
	public bool disableControls = true;	// Whether or not to disable player controls while textbox is showing

	private string[] textLines;
	private Text textTitle;
	private Text theText;
	private int currentLine;
	private int endAtLine;
	private PlayerMovement player;
	
	
	void Start() {
		player = FindObjectOfType<PlayerMovement> ();

		if (textFile != null) {
			textBox.SetActive (true);
			textLines = textFile.text.Split ('\n');
			textTitle = GameObject.Find ("Canvas/Fitter/Panel/Title").gameObject.GetComponent<Text>();
			theText = GameObject.Find ("Canvas/Fitter/Panel/Text").gameObject.GetComponent<Text>();
			if(endAtLine == 0) {
				endAtLine = textLines.Length - 1;
			}
			GlobalVars.controlsEnabled = false;
			GlobalVars.aiEnabled = false;
		} else {
			textBox.SetActive (false);
		}
	}

	void Update()
	{
		if (textBox.activeSelf) {
			string[] line = parseLine(textLines[currentLine]);

			textTitle.text = line[0];
			theText.text = line[1];

			if (Input.GetKeyDown (KeyCode.Return)) {
				currentLine += 1;
			}

			if (currentLine > endAtLine) {
				textBox.SetActive (false);
				GlobalVars.controlsEnabled = true;
				GlobalVars.aiEnabled = true;
			}
		}
	}

	string[] parseLine(string line) {
		string[] lineArr = line.Split(TextDelimiters.title, System.StringSplitOptions.None);
		string title = lineArr [0];
		string body = lineArr [1];

		foreach (string delimiter in TextDelimiters.newline) {
			body = body.Replace(delimiter, "\n");
		}

		return new string[] {title, body};
	}
}
