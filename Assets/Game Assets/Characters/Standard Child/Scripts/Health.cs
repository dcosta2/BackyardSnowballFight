using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;
    public const int maxHealth = 100;

    private const float starting_width = 0.51f;
	private NetworkStartPosition[] spawnPoints;

	[SyncVar]
	public bool m_isDead = false;
	public GameObject m_DeathPrefab;
	public SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter m_lastAttacker;

    void Start()
    {
        
   
				if (isLocalPlayer)
		{
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		}
	}


	public void TakeDamage(int amount)
	{
		if (!isServer) {
			return;
		}
        /*
		if (pc != null && pc != this.GetComponent<SBF.Player.ThirdPerson.SBF_ThirdPersonCharacter> ()) {
			m_lastAttacker = pc;
		} */

		currentHealth -= amount;

		if (currentHealth <= 0 && !m_isDead)
		{
			m_isDead = true;
			/* if (m_lastAttacker != null) {
				m_lastAttacker.m_score++;
				m_lastAttacker = null;
			} */

			GameManager.Instance.UpdateScoreboard ();
			// called on the Server, but invoked on the Clients
			RpcRespawn();
		}
	}

	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health * starting_width, healthBar.sizeDelta.y);
        if(isLocalPlayer)
        {
            FindObjectOfType<PlayerUI>().SetPlayerHealth(health);
        }
	}

	[ClientRpc]
	void RpcRespawn()
	{
        currentHealth = maxHealth;
        if (isLocalPlayer)
		{
			// Set the spawn point to origin as a default value
			Vector3 spawnPoint = Vector3.zero;

			// If there is a spawn point array and the array is not empty, pick one at random
			if (spawnPoints != null && spawnPoints.Length > 0)
			{
				spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			}

			// Set the player’s position to the chosen spawn point
			transform.position = spawnPoint;
            
            m_isDead = false;
        }
	}
}