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
        Vector3 launchPoint = snowballLaunchPoint.transform.position;
        Vector3 projectileVelocity = snowballPrefab.GetComponent<Projectile>().projectileVelocity;
        Vector3 velocity = snowballLaunchPoint.transform.TransformDirection(projectileVelocity);
        CmdLaunchSnowball(gameObject.GetComponent<NetworkIdentity>().netId.ToString(), launchPoint, velocity);
		audioSource.clip = snowballthrowingsound;
		audioSource.Play();
		m_Animator.ResetTrigger("ThrowSnowball"); 
	}

	[Command]
	void CmdLaunchSnowball (string throwing_player, Vector3 launchPoint, Vector3 velocity) {
		GameObject snowball = Instantiate(snowballPrefab, launchPoint, snowballLaunchPoint.transform.rotation) as GameObject;
        snowball.GetComponent<SnowBall>().owner = throwing_player;
        if (snowball != null) {
			Rigidbody snowballRigidBody = snowball.GetComponent<Rigidbody> ();
            snowballRigidBody.velocity = velocity;
			NetworkServer.Spawn (snowball);
		}
	}
}
