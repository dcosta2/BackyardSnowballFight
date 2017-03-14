using UnityEngine;
//using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Projectile))]
public class SnowBall : NetworkBehaviour {

    public GameObject BrokenSnowball;
    public int owner;

    private Projectile projectile;
    private GameManager gameManager;

    public void Start()
    {
        projectile = GetComponent<Projectile>();
        gameManager = FindObjectOfType<GameManager>();
    }
 
	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag != "Player"){
			gameObject.SetActive(false);
			Instantiate(BrokenSnowball, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		} else
        {
            int hitPlayer = coll.gameObject.GetComponent<PlayerSetup>().m_playerNum;
            if (hitPlayer == owner)
            {
                Debug.Log("Stop Hitting yourself!");
                return;
            } else {
                gameObject.SetActive(false);
                Instantiate(BrokenSnowball, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                gameManager.NumberToPlayerSetup(owner-1).m_score += projectile.damage;
                projectile.DealDamage(coll);
            }
        }
	}


}