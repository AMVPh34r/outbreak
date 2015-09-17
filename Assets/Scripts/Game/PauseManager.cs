using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
    private Canvas canvas;

    void Start() {
        this.canvas = GetComponent<Canvas>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.canvas.enabled = !this.canvas.enabled;
            //Pause();
            Quit();
        }
    }

    public void Pause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Quit() {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
