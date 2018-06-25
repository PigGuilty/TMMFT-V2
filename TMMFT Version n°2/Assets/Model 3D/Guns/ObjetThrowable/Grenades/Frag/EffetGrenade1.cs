using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class EffetGrenade1 : MonoBehaviour {

    public GameObject Explosion;

    private GameObject Player;
    private PrendreObjet prendreobjet;

    public bool canExplode;
    private bool DébutDeLAction;

    private float timer;
	private float randNumb;

    public BoxCollider boxTrigger;
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("localPlayer");
		netSpawner = Player.GetComponent<NetworkSpawner>();
		m_isServer = Player.GetComponent<FirstPersonController>().isServer;

        DébutDeLAction = false;
        canExplode = false;
        prendreobjet = Player.GetComponent<PrendreObjet>();

		randNumb = Random.Range (2.5f, 3.0f);
    }

    private void Update()
    {
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
			if(timer >= randNumb)
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
