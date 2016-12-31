using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashPanel : MonoBehaviour {

	public Image myImage;
	public float fadeTime = 1f;
	public Text subtitle;

	void Start () {
		myImage.GetComponent<CanvasRenderer>().SetAlpha(0.001f);
		subtitle.GetComponent<CanvasRenderer>().SetAlpha(0.001f);
		Color visible = myImage.color;
		visible.a = 1f;
		myImage.color = visible;
		visible = subtitle.color;
		visible.a = 1f;
		subtitle.color = visible;
		myImage.CrossFadeAlpha(1.0f, fadeTime/2, false);
		Invoke("subtitleFadeIn", fadeTime/2);
	}

	void subtitleFadeIn () {
		subtitle.CrossFadeAlpha(1.0f, fadeTime/2, false);
	}
}
