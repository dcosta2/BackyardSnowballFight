using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] musicLevelChangeArray;

	private AudioSource music;
	
	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource>();
		music.volume = PlayerPrefsManager.GetMasterVolume();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnLevelWasLoaded(int level){
		AudioClip thisLevelsMusic = musicLevelChangeArray[level];
		if (thisLevelsMusic) {
			music.Stop ();
			music.clip = thisLevelsMusic;
			music.loop = true;
			music.Play ();	
		}
	}
	
	public void SetVolume (float volume) {
		music.volume = volume;
	}
}
