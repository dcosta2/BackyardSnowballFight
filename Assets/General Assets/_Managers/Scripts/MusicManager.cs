using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	
	/* void OnLevelWasLoaded(int level){
        AudioClip thisLevelsMusic = musicLevelChangeArray[level];
        if (thisLevelsMusic)
        {
            music.Stop();
            music.clip = thisLevelsMusic;
            music.loop = true;
            music.Play();
        }
    } */
	
	public void SetVolume (float volume) {
		music.volume = volume;
	}

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
   
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        AudioClip thisLevelsMusic = musicLevelChangeArray[SceneManager.GetActiveScene().buildIndex];
        if ((thisLevelsMusic) && (music != null))
        {
            music.Stop();
            music.clip = thisLevelsMusic;
            music.loop = true;
            music.Play();
        }
    }
}
