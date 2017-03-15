using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour {

    [SyncVar]
    public int m_score;

    [SyncVar(hook="UpdateName")]
	public int m_playerNum;

    public string m_basename = "PLAYER ";
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
	}

	void Update() {
		UpdateName(m_playerNum);
	}

	public void UpdateName (int pNum)
	{
            if (isLocalPlayer) { 
            m_playerNum = pNum;
            if (m_playerNameText != null)
            {
                m_playerNameText.enabled = true;
                if (PlayerPrefsManager.GetPlayerName().Length > 0)
                {
                    m_playerNameText.text = PlayerPrefsManager.GetPlayerName();
                }
                else
                {
                    m_playerNameText.text = m_basename + " " + m_playerNum.ToString();
                }
            }
            }
            if ((isLocalPlayer) && (FindObjectOfType<PlayerUI>() != null)) {
                if (PlayerPrefsManager.GetPlayerName().Length > 0) {
                    FindObjectOfType<PlayerUI>().SetPlayerName(PlayerPrefsManager.GetPlayerName());
                } else {
                    FindObjectOfType<PlayerUI>().SetPlayerName(m_basename + " " + m_playerNum.ToString());
                }
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
