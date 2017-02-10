using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {

	public Transform m_target;
	public float m_lookSmooth = 0.5f;

    float rotateVel;

    void LateUpdate() {
    	// Rotate the camera every frame so it keeps looking at the target 
        //transform.LookAt(target);

		float eulerYAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, m_target.eulerAngles.y, ref rotateVel, m_lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
    }
}
