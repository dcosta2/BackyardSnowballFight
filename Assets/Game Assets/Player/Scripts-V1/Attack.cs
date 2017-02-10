using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

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
		GameObject snowball = Instantiate(snowballPrefab, snowballLaunchPoint.transform.position, snowballLaunchPoint.transform.rotation) as GameObject;
		Rigidbody snowballRigidBody = snowball.GetComponent<Rigidbody>();
		snowballRigidBody.velocity = snowballLaunchPoint.transform.TransformDirection(new Vector3 (0, 1, 15));
		audioSource.clip = snowballthrowingsound;
		audioSource.Play();

		// *** Begin Test Code ***
		// *** Begin Test Code ***

		// Trying to stop double throws from happening. Only Partially effective. Maybe invoke a separate function towards the end of the animation or just run a "clearTrigger" function from the animation instead. Considering options.

		m_Animator.ResetTrigger("ThrowSnowball"); 

		// *** End Test Code ***
		// *** End Test Code ***
	}
}
