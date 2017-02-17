using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.Networking;

public class Attack : NetworkBehaviour {

	public GameObject snowballPrefab;
	public GameObject snowballLaunchPoint;
	public AudioClip snowballthrowingsound;

	private AudioSource audioSource;

	// Part of Test Code Solution
	// Part of Test Code Solution

	private Animator m_Animator;

	// Part of Test Code Solution
	// Part of Test Code Solution

	void Start () {
		audioSource = GetComponent<AudioSource>();
	
		// Part of Test Code Solution
		// Part of Test Code Solution

		m_Animator = GetComponent<Animator>();

		// Part of Test Code Solution
		// Part of Test Code Solution

	}

	public void LaunchSnowball () {
		CmdLaunchSnowball(gameObject.GetComponent<NetworkIdentity>().netId.ToString());
		audioSource.clip = snowballthrowingsound;
		audioSource.Play();
		m_Animator.ResetTrigger("ThrowSnowball"); 
	}

	[Command]
	void CmdLaunchSnowball (string throwing_player) {
		GameObject snowball = Instantiate(snowballPrefab, snowballLaunchPoint.transform.position, snowballLaunchPoint.transform.rotation) as GameObject;
        snowball.GetComponent<SnowBall>().throwingPlayer = throwing_player;
        if (snowball != null) {
			Rigidbody snowballRigidBody = snowball.GetComponent<Rigidbody> ();
			snowballRigidBody.velocity = snowballLaunchPoint.transform.TransformDirection (new Vector3 (0, 1, 15));
			NetworkServer.Spawn (snowball);
		}
	}
}
