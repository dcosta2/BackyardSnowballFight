using UnityEngine;
//using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Projectile))]
public class SnowBall : NetworkBehaviour {

    public GameObject particleSytem;
    public string owner;

    private Projectile projectile;

    public void Start()
    {
        projectile = GetComponent<Projectile>();
    }
 
	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag != "Player"){
			gameObject.SetActive(false);
			GameObject explodeEffect = Instantiate(particleSytem, gameObject.transform.position, Quaternion.identity);
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
                GameObject explodeEffect = Instantiate(particleSytem, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                projectile.DealDamage(coll);
            }
        }
	}


}