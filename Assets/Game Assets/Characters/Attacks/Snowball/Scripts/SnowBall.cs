using UnityEngine;
//using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Projectile))]
public class SnowBall : NetworkBehaviour {

    public GameObject BrokenSnowball;
    public string owner;

    private Projectile projectile;

    public void Start()
    {
        projectile = GetComponent<Projectile>();
    }
 
	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag != "Player"){
			gameObject.SetActive(false);
			Instantiate(BrokenSnowball, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		} else
        {
            string hitPlayer = coll.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
            if (hitPlayer == owner)
            {
                Debug.Log("Stop Hitting yourself!");
                return;
            } else {
                gameObject.SetActive(false);
                Instantiate(BrokenSnowball, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                projectile.DealDamage(coll);
            }
        }
	}


}