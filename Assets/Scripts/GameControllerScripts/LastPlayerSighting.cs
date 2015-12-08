using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour
{
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);			// The last global sighting of the player.
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);	// The default position if the player is not in sight.
	public float lightHighIntensity = 0.25f;							// The directional light's intensity when the alarms are off.
	public float lightLowIntensity = 0f;								// The directional light's intensity when the alarms are on.
	public float fadeSpeed = 7f;										// How fast the light fades between low and high intensity.
	public float musicFadeSpeed = 1f;									// The speed at which the panic music fades in
	public float panicAudioVolume = 0.8f;								// The maximum volume for panic music to play
	public float alarmAudioVolume = 0.1f;								// The maximum volume for the alarm to play

	private AlarmLight alarm;											// Reference to the AlarmLight script.
	private Light mainLight;											// Reference to the main light.
	private AudioSource panicAudio;										// Reference to the AudioSource of the panic msuic.
	private AudioSource alarmAudio;										// Reference to the AudioSource of the alarm.
	private AudioSource[] sirens;										// Reference to the AudioSources of the megaphones.
	
	
	void Awake ()
	{
		// Setup the reference to the alarm light.
		alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
		
		// Setup the reference to the main directional light in the scene.
		mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
		
		// Setup the reference to the additonal audio source.
		panicAudio = transform.FindChild ("secondaryMusic").GetComponent<AudioSource>();

		// Setup the reference to the alarm.
		alarmAudio = transform.FindChild ("alarmSFX").GetComponent<AudioSource>();
		
		// Find an array of the siren gameobjects.
		// TODO fix
		//GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
		
		// Set the sirens array to have the same number of elements as there are gameobjects.
		// TODO fix
		//sirens = new AudioSource[sirenGameObjects.Length];
		
		// For all the sirens allocate the audio source of the gameobjects.
		// TODO fix
		/*
		 * for(int i = 0; i < sirens.Length; i++)
		{
			sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
		}
		 */
	}
	
	
	void Update ()
	{
		// Switch the alarms and fade the music.
		SwitchAlarms();
		MusicFading();
	}
	
	
	void SwitchAlarms ()
	{
		// Set the alarm light to be on or off.
		alarm.alarmOn = position != resetPosition;
		
		// Create a new intensity.
		float newIntensity;
		
		// If the position is not the reset position...
		if(position != resetPosition)
			// ... then set the new intensity to low.
			newIntensity = lightLowIntensity;
		else
			// Otherwise set the new intensity to high.
			newIntensity = lightHighIntensity;
		
		// Fade the directional light's intensity in or out.
		mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);
		
		// For all of the sirens...
		// TODO fix
		/*
		for(int i = 0; i < sirens.Length; i++)
		{
			// ... if alarm is triggered and the audio isn't playing, then play the audio.
			if(position != resetPosition && !sirens[i].isPlaying)
				sirens[i].Play();
			// Otherwise if the alarm isn't triggered, stop the audio.
			else if(position == resetPosition)
				sirens[i].Stop();
		}
		*/
	}
	
	
	void MusicFading ()
	{
		// If the alarm is not being triggered...
		if(position != resetPosition)
		{
			// ... fade out the normal music, fade in panic music, and play alarm
			GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0f, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp(panicAudio.volume, panicAudioVolume, musicFadeSpeed * Time.deltaTime);
			alarmAudio.volume = Mathf.Lerp(alarmAudio.volume, alarmAudioVolume, musicFadeSpeed * Time.deltaTime);
		}
		else
		{
			// Otherwise fade in the normal music and fade out the panic music.
			GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0.8f, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
			alarmAudio.volume = Mathf.Lerp(alarmAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
		}
	}
}
