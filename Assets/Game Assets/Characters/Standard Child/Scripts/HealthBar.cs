using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Transform h_target;
    private float m_lookSmooth = 0.01f;

    float rotateVel;

    private void Start () {
        findActiveCamera();
    }

    private void Update()
    {
        //if (h_target.gameObject.name != "FollowCamera") {
            findActiveCamera();
        //}
    }

    private void findActiveCamera()
    {
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera camera in cameras)
        {
            if (camera.enabled == true)
            {
                h_target = camera.gameObject.transform;
            }
        }
    }

    void LateUpdate()
    {
        // Rotate the canvas every frame so it keeps looking at the target 
        if (h_target)
        {
            float eulerYAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, h_target.eulerAngles.y, ref rotateVel, m_lookSmooth);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
        }
    }
}
