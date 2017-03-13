using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float fuselength=7f;

	float timesincecreation;
	// Use this for initialization
	void Start () {
			timesincecreation = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timesincecreation += Time.deltaTime;
		if (timesincecreation > fuselength) {
			Destroy(gameObject);
		}
	}
}
