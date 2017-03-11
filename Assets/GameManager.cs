using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour 
{

	public Text m_messageText;
	public int m_minPlayers = 1;
	public int m_maxPlayers = 4;

	[SyncVar]
	public int m_playerCount = 0;

	public Color[] m_playerColors = {Color.red, Color.blue, Color.green, Color.magenta};

	static private GameManager instance;
	public List<SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter> m_allPlayers;

	public List<Text> m_nameLabelText;
	public List<Text> m_playerScoreText;
	public int m_maxScore = 3;

	[SyncVar]
	bool m_gameOver = false;

	SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter m_winner;

	static public GameManager Instance 
	{		
		get {
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<GameManager>();
				if (instance == null) 
				{
					instance = new GameObject().AddComponent<GameManager>();
				}
			}

			return instance;
		}
	}

	void Awake () 
	{
		if (instance == null)
		{
			instance = this;
		} else 
		{
			Destroy(gameObject);
		}
	}

	void Start() 
	{
		StartCoroutine("GameLoopRoutine");
	}

	IEnumerator GameLoopRoutine()
	{
		yield return StartCoroutine("EnterLobby");
		yield return StartCoroutine("PlayGame");
		yield return StartCoroutine("EndGame");
	}

	IEnumerator EnterLobby ()
	{
		DisablePlayers();
		while (m_playerCount < m_minPlayers){
			UpdateMessage("Waiting for Players");
			DisablePlayers();
			yield return null;
		}
		DisablePlayers();
	}

	IEnumerator PlayGame ()
	{
		UpdateMessage("Get Ready to Rumble");
		yield return new WaitForSeconds(2f);
		UpdateMessage("3");
		yield return new WaitForSeconds(1f);
		UpdateMessage("2");
		yield return new WaitForSeconds(1f);
		UpdateMessage("1");
		yield return new WaitForSeconds(1f);
		UpdateMessage("Fight");
		yield return new WaitForSeconds(1f);
		UpdateMessage("");
		EnablePlayers();
		UpdateScoreboard();

		m_winner = null;

		while (m_gameOver == false) {
			yield return null;
		}
	}

	IEnumerator EndGame ()
	{
		UpdateMessage("GAME OVER \n The Winner is: Player " + m_winner.m_pSetup.m_playerNum.ToString() + "!!");
		DisablePlayers();

		yield return null;
	}


	void SetPlayerState(bool state) {
		SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter[] allPlayers = GameObject.FindObjectsOfType<SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter>();
		foreach (SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter p in allPlayers) {
			p.enabled = state;
		}
	}

	void EnablePlayers() {
		SetPlayerState(true);
	}

	void DisablePlayers() {
		SetPlayerState(false);
	}

	public void AddPlayer(PlayerSetup pSetup)
	{
		if(m_playerCount < m_maxPlayers) {
			m_allPlayers.Add(pSetup.GetComponent<SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter>());
			pSetup.m_playerColor = m_playerColors[m_playerCount];
			pSetup.m_playerNum = m_playerCount + 1;
		}
	}

	[ClientRpc]
	void RpcUpdatesScoreBoard(string[] playerNames, int[] playerScores) {
		for (int i=0; i < m_playerCount; i++) {
			if (playerNames[i] != null) {
				m_nameLabelText[i].text = playerNames[i];
			}

			if (playerNames[i] != null) {
				m_playerScoreText[i].text = playerScores[i].ToString();
			}
		}

	}

	public void UpdateScoreboard () {
		if (isServer) {

			m_winner = GetWinner();
			if (m_winner != null) {
				m_gameOver = true;
			}
			string[] names = new string[m_playerCount];
			int[] scores = new int[m_playerCount];
			for (int i=0; i<m_playerCount; i++) {
				names[i] = m_allPlayers[i].GetComponent<PlayerSetup>().m_playerNameText.text;
				scores[i] = m_allPlayers[i].m_score;
			}
			RpcUpdatesScoreBoard(names, scores);
		}
	}

	public SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter GetWinner() {
		if (isServer) {
			for (int i=0; i< m_playerCount; i++) {
				if (m_allPlayers[i].m_score >= m_maxScore) {
					return m_allPlayers[i];
				}
			}
		}
		return null;
	}

	[ClientRpc]
	void RpcUpdateMessage(string msg) {
		if (m_messageText !=null) {
			m_messageText.gameObject.SetActive(true);
			m_messageText.text = msg;
		}
	}

	public void UpdateMessage(string msg) {
		if (isServer) 
		{
			RpcUpdateMessage(msg);
		}
	}
}
