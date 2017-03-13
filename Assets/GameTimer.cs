using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameTimer : NetworkBehaviour {

    [SyncVar]
    public float currentTime;
    [SyncVar]
    public bool gameOver = false;
    [SyncVar]
    private bool gameStarted = false;

    public Text text;
    

	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStarted) {
            currentTime -= Time.deltaTime;
            if (currentTime > 0 && !gameOver) {
                SetTime(currentTime);
            } else {
                SetTimeText("Game Over");
                text.color = Color.red;
                gameOver = true;
                gameStarted = false;
            }
        }
	}

    public void SetTotalTime (float time) {
        currentTime = time;
        SetTime(currentTime);
    }

    public void StartTimer () {
        gameStarted = true;
    }

    void SetTime(float time)
    {
        string minSec = string.Format("{0}:{1:00}", (int)time / 60, (int)time % 60);
        SetTimeText(minSec);
    }

    void SetTimeText(string setTimetext)
    {
        text.text = setTimetext;
    }
}
