using UnityEngine;
using System.Collections;

public class SnowBall : MonoBehaviour {
	public GameObject particleSytem;
	// Use this for initialization

	void Start () {
		 

	}
	
	// Update is called once per frame
	void Update () {
		
		}
	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag != "Player"){
			gameObject.SetActive(false);
			GameObject explodeEffect = Instantiate(particleSytem, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}