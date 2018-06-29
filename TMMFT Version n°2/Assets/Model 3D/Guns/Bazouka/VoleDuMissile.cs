using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class VoleDuMissile : NetworkBehaviour {

    public ParticleSystem EffetFusée;
    public GameObject Explosion;

    private ArmoireScript armoirescript;

	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
	private FirstPersonController player;
	
    // Use this for initialization
    void Start () {
        EffetFusée.Play();
		GameObject p = GameObject.FindWithTag("localPlayer");
		netSpawner = p.GetComponent<NetworkSpawner>();
		
		player = p.GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.isLocalPlayer)
			transform.Translate(-Vector3.forward * Time.deltaTime * 20);
		
		m_isServer = player.isServer;
    }

    void OnCollisionEnter(Collision other) {		
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "localPlayer") {
            if (other.gameObject.GetComponent<ArmoireScript>() != null)
            {
                armoirescript = other.gameObject.GetComponent<ArmoireScript>();

                if (armoirescript.Ouvert == false)
                {
					if(m_isServer){
						GameObject item = Instantiate(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
						NetworkServer.Spawn(item);
					}else{
						netSpawner.Spawn(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal), -1, "");
					}
					
					Destroy(gameObject);
                }
            }
            else
            {
				if(m_isServer){
					GameObject item = Instantiate(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
					NetworkServer.Spawn(item);
				}else{
					netSpawner.Spawn(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal), -1, "");
				}
				
				Destroy(gameObject);
            }
        }
    }
}
