using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform m_target;
	public Transform m_cameratarget;
	public float m_lookSmooth = 0.09f;
	public Vector3 m_offsetFromTarget = new Vector3(0, 0.7034f, -1.5f);
	public float m_xTilt = 10; 

	Vector3 m_destination = Vector3.zero;
	CharacterController m_charController;
	float m_rotateVel = 0;

	// Use this for initialization
	void Start () {
		SetCameraTarget(m_target);	
		/*Vector3 temp = transform.position - m_target.position;
		m_offsetFromTarget = temp;*/
	}

	// Update is called once per frame
	void Update () {
		
	}

	void SetCameraTarget(Transform t) {
		if (t != null)
		{
			CharacterController temp = t.GetComponent<CharacterController>();
			if(temp) {
				m_target = t;
				m_charController = temp;
			} else {
				Debug.Log("SetCameraTarget: Camera's Target has to have a character controller attached to it");
			}
		} else {
			Debug.Log("SetCameraTarget: Tried to Set Camera to a null Target");
		}
	}

	void LateUpdate () {
		MoveToTarget ();
		LookAtTarget ();
	}

	void MoveToTarget () {
		m_destination = m_charController.TargetRotation * m_offsetFromTarget;
		m_destination += m_target.position;
		transform.position = m_destination;
	}

	void LookAtTarget() {
		transform.LookAt(m_cameratarget);
	}
}
