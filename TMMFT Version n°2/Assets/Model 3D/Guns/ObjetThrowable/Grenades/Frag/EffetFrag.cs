using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class EffetFrag : MonoBehaviour {

	public int NombreDeGrenades;

	public GameObject FragmentGrenade;
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
		if (Player != null){
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
					for (int i = 0; i < NombreDeGrenades; i++){
						Vector3 pos = gameObject.transform.position + new Vector3(Random.Range(-0.35f,0.35f), Random.Range(-0.35f,0.35f), Random.Range(-0.35f,0.35f));
						if(m_isServer){
							GameObject Frag = Instantiate(FragmentGrenade, pos, Quaternion.identity);
							NetworkServer.Spawn(Frag);
						}else{
							netSpawner.Spawn(FragmentGrenade, pos, Quaternion.identity, -1, "");
						}
					}

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
			if (other.gameObject.tag == "Bullet")
			{
				for (int i = 0; i < NombreDeGrenades; i++){
					Vector3 pos = gameObject.transform.position + new Vector3(Random.Range(-0.35f,0.35f), Random.Range(-0.35f,0.35f), Random.Range(-0.35f,0.35f));
					if(m_isServer){
						GameObject Frag = Instantiate(FragmentGrenade, pos, Quaternion.identity);
						NetworkServer.Spawn(Frag);
					}else{
						netSpawner.Spawn(FragmentGrenade, pos, Quaternion.identity, -1, "");
					}
				}
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
