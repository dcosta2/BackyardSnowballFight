using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

namespace SBF.Player.ThirdPerson
{
	[RequireComponent(typeof (SBF_ThirdPersonCharacter))]
	[RequireComponent(typeof (Animator))]

	public class SBF_ThirdPersonUserControl : NetworkBehaviour
    {
		private SBF_ThirdPersonCharacter m_Character;

        private Vector3 m_Move;
		private Animator m_Animator;

        
        private void Start()
        {
			m_Animator = GetComponent<Animator>();
			m_Character = GetComponent<SBF_ThirdPersonCharacter>();
        }

        private void FixedUpdate()
        {
			if (isLocalPlayer) {
				float h = CrossPlatformInputManager.GetAxis ("Horizontal");
				float v = CrossPlatformInputManager.GetAxis ("Vertical");

				if (Input.GetKeyDown (KeyCode.F)) {
					m_Animator.SetTrigger ("ThrowSnowball");
				}

				m_Move = v * transform.forward + h * transform.right;
				m_Character.Move (m_Move);
			}
        }
    }
}
