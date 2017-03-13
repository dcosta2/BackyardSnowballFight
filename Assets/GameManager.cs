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
    public float GameDuration;

    [SyncVar]
	public int m_playerCount = 0;
    
	static private GameManager instance;
	private List<PlayerSetup> m_allPlayers = new List<PlayerSetup>();

    private List<Text> m_nameLabelText;
    private List<Text> m_playerScoreText;
    private int m_maxScore = 3;
    private PlayerUI playerUI;
    private GameTimer timer;


    [SyncVar]
	bool m_gameOver = false;

	PlayerSetup m_winner;

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
        timer = GetComponent<GameTimer>();
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
        timer.SetTotalTime(GameDuration);
        timer.StartTimer();
		
		//UpdateScoreboard();

		//m_winner = null;      
        while ((m_gameOver == false) && (timer.gameOver == false)) {
			yield return null;
		}

        m_gameOver = true;
	}

	IEnumerator EndGame ()
	{
		UpdateMessage("GAME OVER \n The Winner is: Player " /*+ m_winner.m_pSetup.m_playerNum.ToString()*/ + "!!");
		DisablePlayers();
        Camera camera = GameObject.Find("LobbyCamera").GetComponent<Camera>();
        camera.enabled = true;
        yield return null;
	}


	void SetPlayerState(bool state) {
		SBF.Player.ThirdPerson.SBF_ThirdPersonUserControl[] allPlayers = GameObject.FindObjectsOfType<SBF.Player.ThirdPerson.SBF_ThirdPersonUserControl>();
		foreach (SBF.Player.ThirdPerson.SBF_ThirdPersonUserControl player in allPlayers) {
			player.enabled = state;
            player.gameObject.SetActive(state);
        }
	}

	void EnablePlayers() {
		SetPlayerState(true);
        if (playerUI == null) { playerUI = FindObjectOfType<PlayerUI>(); }
        playerUI.gameObject.SetActive(true);
	}

	void DisablePlayers() {
		//SetPlayerState(false);
        if (playerUI == null) { playerUI = FindObjectOfType<PlayerUI>(); }
        playerUI.gameObject.SetActive(false);
    }

	public void AddPlayer(PlayerSetup pSetup)
	{
		if((m_playerCount < m_maxPlayers) && (pSetup != null)) {
			m_allPlayers.Add(pSetup);
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
				names[i] = m_allPlayers[i].m_playerNameText.text;
				scores[i] = m_allPlayers[i].m_score;
			}
			RpcUpdatesScoreBoard(names, scores);
		}
	}

	public PlayerSetup GetWinner() {
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
