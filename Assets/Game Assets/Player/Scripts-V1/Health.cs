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
    private PlayerUI playerUI;

    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }

<<<<<<< HEAD
	private NetworkStartPosition[] spawnPoints;

	void Start ()
	{
		if (isLocalPlayer)
		{
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		}
	}

	public void TakeDamage(int amount)
=======
    public void TakeDamage(int amount)
>>>>>>> e9438edb38da03149f7d4cb9e671372c994e478b
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = maxHealth;

			// called on the Server, but invoked on the Clients
			RpcRespawn();
		}
	}

	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health * starting_width, healthBar.sizeDelta.y);
        if(isLocalPlayer)
        {
            Debug.Log("In isLocalPlayer Loop, trying to set health to" + health);
            playerUI.SetPlayerHealth(health);
        }
	}
	[ClientRpc]
	void RpcRespawn()
	{
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
		}
	}
}