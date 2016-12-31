using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

	public float fadeInTime;
	
	private Image fadePanel;
	private Color currentColor = Color.white;
	
	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad < fadeInTime) {
			// Fade In
			float currentAlpha = 1f - (Time.timeSinceLevelLoad)/fadeInTime;
			currentColor.a = currentAlpha;
//			float alphaChange = Time.deltaTime/fadeInTime;
//			currentColor.a -= alphaChange;
//			Debug.Log(currentColor.a);
			fadePanel.color = currentColor;
		} else {
			gameObject.SetActive (false);
		}
	}
}
