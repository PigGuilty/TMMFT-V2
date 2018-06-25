using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class EffetGrenade4 : MonoBehaviour {

    public GameObject Explosion;

    private GameObject Player;
    private PrendreObjet prendreobjet;

    public bool canExplode;
    private bool DébutDeLAction;

    private float timer;
	
    public BoxCollider boxTrigger;
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("localPlayer");

        DébutDeLAction = false;
        canExplode = false;
		if(Player != null){
			prendreobjet = Player.GetComponent<PrendreObjet>();
			
			netSpawner = Player.GetComponent<NetworkSpawner>();
			m_isServer = Player.GetComponent<FirstPersonController>().isServer;
		}
    }

    private void Update()
    {
		if (Player == null){//update player
			Player = GameObject.FindWithTag("localPlayer");
			if(Player != null){
				prendreobjet = Player.GetComponent<PrendreObjet>();
				
				netSpawner = Player.GetComponent<NetworkSpawner>();
				m_isServer = Player.GetComponent<FirstPersonController>().isServer;
			}
		}else{
			if (boxTrigger.enabled == true)
			{
				DébutDeLAction = true;
			}

			if (prendreobjet.ObjetPris == false && DébutDeLAction == true)
			{
				canExplode = true;
				gameObject.tag = "Untagged"; 
			}

			if (canExplode == true)
			{
				if(timer >= 3)
				{
					if(m_isServer){
						GameObject Explo = Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
						NetworkServer.Spawn(Explo);
					}else{
						netSpawner.Spawn(Explosion, gameObject.transform.position, Quaternion.identity, -1, "");
					}
					Destroy(gameObject);
				}
				timer += Time.deltaTime;
			}
		}
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canExplode == true)
        {
			if (other.gameObject.tag == "Bullet") {
                if(m_isServer){
					GameObject Explo = Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
					NetworkServer.Spawn(Explo);
				}else{
					netSpawner.Spawn(Explosion, gameObject.transform.position, Quaternion.identity, -1, "");
				}
                Destroy(gameObject);
			}
        }
    }
}
