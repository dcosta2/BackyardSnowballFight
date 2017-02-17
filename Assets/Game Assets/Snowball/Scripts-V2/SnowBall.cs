using UnityEngine;
//using System.Collections;
using UnityEngine.Networking;

public class SnowBall : NetworkBehaviour {
	public GameObject particleSytem;
    // Use this for initialization

    public int damage = 10;
    public string throwingPlayer;

	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag != "Player"){
			gameObject.SetActive(false);
			GameObject explodeEffect = Instantiate(particleSytem, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		} else
        {
            string hitPlayer = coll.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
            if (hitPlayer == throwingPlayer)
            {
                Debug.Log("Stop Hitting yourself!");
                return;
            } else {
                DealDamage(coll);
            }
        }
	}

    public void DealDamage(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        gameObject.SetActive(false);
        GameObject explodeEffect = Instantiate(particleSytem, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}