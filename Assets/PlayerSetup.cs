using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour {

	[SyncVar(hook="UpdateColor")]
	public Color m_playerColor = Color.blue;
	public string m_basename = "PLAYER ";

	[SyncVar(hook="UpdateName")]
	public int m_playerNum;
	public Text m_playerNameText;

	public override void OnStartClient() {
		base.OnStartClient();
		if (m_playerNameText != null) {
			m_playerNameText.enabled = false;
		}
	}
	public override void OnStartLocalPlayer() {

		base.OnStartLocalPlayer();
		CmdSetupPlayer();
		UpdateName(m_playerNum);
	}

	void Start() 
	{
		//	if (!isLocalPlayer) {
		UpdateName(m_playerNum);
		UpdateColor(m_playerColor);
		//	}
	}

	void Update() {
		//	UpdateName(m_playerNum);
	}

	public void UpdateColor (Color pColor)
	{
		MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer r in meshes) {
			r.material.color = pColor;
		}
	}

	public void UpdateName (int pNum)
	{
		m_playerNum = pNum;
		if (m_playerNameText != null) {
			m_playerNameText.enabled = true;
			m_playerNameText.text = m_basename + " " + m_playerNum.ToString ();
		}
	}

	[Command]
	void CmdSetupPlayer() 
	{
		GameManager.Instance.AddPlayer(this);
		GameManager.Instance.m_playerCount++;
		UpdateName(m_playerNum);
	}
}
