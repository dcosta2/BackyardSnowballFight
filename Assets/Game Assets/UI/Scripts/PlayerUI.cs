using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public RectTransform playerHealthbar;
    public Text playerName;

    public void SetPlayerHealth(int newHealth)
    {
        playerHealthbar.sizeDelta = new Vector2(playerHealthbar.sizeDelta.x, newHealth);
    }

    public void SetPlayerName(string name) {
        playerName.text = name;
    }
}

