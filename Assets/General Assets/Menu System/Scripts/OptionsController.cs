﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider difficultySlider;
    public Text playerNameText;
    public Text placeholder;

    private LevelManager levelManager;
    private MusicManager musicManger;
	
	// Use this for initialization
	
	void Start () {
		musicManger = GameObject.FindObjectOfType<MusicManager>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        placeholder.text = PlayerPrefsManager.GetPlayerName();
        levelManager = FindObjectOfType<LevelManager>();
	}
		
	public void Default() {
		PlayerPrefsManager.SetMasterVolume(0.8f);
		PlayerPrefsManager.SetDifficulty(1f);
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        placeholder.text = PlayerPrefsManager.GetPlayerName();
        playerNameText.text = PlayerPrefsManager.GetPlayerName();
    }
	public void SaveAndExit () {
//		Debug.Log("volumeSlider.value ="+volumeSlider.value as string);
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
//		Debug.Log ("Master Volume value from prefs ="+PlayerPrefsManager.GetMasterVolume() as string);
		PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        PlayerPrefsManager.SetPlayerName(playerNameText.text);
		levelManager.LoadLevel("01a Start");
	}
	
	void Update() {
        if (musicManger != null)
        {
            musicManger.SetVolume(volumeSlider.value);
        }
	}
}
