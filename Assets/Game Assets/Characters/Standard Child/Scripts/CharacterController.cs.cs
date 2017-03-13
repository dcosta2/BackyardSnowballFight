using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : NetworkBehaviour
{

    public float m_inputDelay = 0.1f;
    public float m_forwardVelocity = 12;
    public float m_rotateVelocity = 100;

    Quaternion m_targetRotation;
    Rigidbody m_rigidbody;
    float m_forwardInput, m_turnInput;

    public Quaternion TargetRotation
    {
        get { return m_targetRotation; }
    }

    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_targetRotation = transform.rotation;

        m_forwardInput = m_turnInput = 0;
    }

    void GetInput()
    {
        m_forwardInput = Input.GetAxis("Vertical");
        m_turnInput = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (Mathf.Abs(m_forwardInput) > m_inputDelay)
        {
            m_rigidbody.velocity = transform.forward * m_forwardInput * m_forwardVelocity;
        }
        else
        {
            // m_rigidbody.velocity = Vector3.zero;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(m_turnInput) > m_inputDelay)
        {
            m_targetRotation *= Quaternion.AngleAxis(m_rotateVelocity * m_turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = m_targetRotation;
    }
}