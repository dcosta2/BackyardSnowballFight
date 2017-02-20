using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    public RectTransform playerHealthbar;

    public void SetPlayerHealth(int newHealth)
    {
        Debug.Log("In SetPlayerHealthScript, trying to set health bar to " + newHealth);
        playerHealthbar.sizeDelta = new Vector2(playerHealthbar.sizeDelta.x, newHealth);
    }
}
