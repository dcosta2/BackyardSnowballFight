using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {

    public float duration = .5f;

    private Text text;


    private void Start()
    {
        text = GetComponent<Text>();
        FlashTheText();
    }

    IEnumerator FlashTheText () {
        string textString = text.text;
		while (true)
        {
            text.text = "";
            yield return new WaitForSeconds(duration);
            text.text = textString;
            yield return new WaitForSeconds(duration);
        }
	}
}
