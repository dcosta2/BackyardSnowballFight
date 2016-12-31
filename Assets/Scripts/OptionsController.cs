using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider difficultySlider;
	public LevelManager levelManager;
	
	private MusicManager musicManger;
	
	// Use this for initialization
	
	void Start () {
		musicManger = GameObject.FindObjectOfType<MusicManager>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}
		
	public void Default() {
		PlayerPrefsManager.SetMasterVolume(0.8f);
		PlayerPrefsManager.SetDifficulty(1f);
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}
	public void SaveAndExit () {
//		Debug.Log("volumeSlider.value ="+volumeSlider.value as string);
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
//		Debug.Log ("Master Volume value from prefs ="+PlayerPrefsManager.GetMasterVolume() as string);
		PlayerPrefsManager.SetDifficulty(difficultySlider.value);
		levelManager.LoadLevel ("01aStart");
	}
	
	void Update() {
		musicManger.SetVolume (volumeSlider.value);
	}
}
