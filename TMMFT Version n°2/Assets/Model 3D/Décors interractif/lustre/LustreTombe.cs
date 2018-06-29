using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class LustreTombe : MonoBehaviour {

    public GameObject Explosion;

    Rigidbody rb;
	AudioSource audio;

	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
	private GameObject player;
	
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource> ();
		
		player = GameObject.FindWithTag("localPlayer");
		
		if(player != null){
			netSpawner = player.GetComponent<NetworkSpawner>();
			
			m_isServer = player.GetComponent<FirstPersonController>().isServer;
		}
	}
	
	void Update() {
		if(player == null){
			player = GameObject.FindWithTag("localPlayer");
			
			if(player != null){
				netSpawner = player.GetComponent<NetworkSpawner>();
				
				m_isServer = player.GetComponent<FirstPersonController>().isServer;
			}
		}
	}

    public void HitByRaycast ()
    {
        rb.isKinematic = false;
		audio.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Objet")
        {
			if(m_isServer){
				GameObject item = Instantiate(Explosion, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
				NetworkServer.Spawn(item);
			}else{
				netSpawner.Spawn(Explosion, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal), -1, "");
			}
			Destroy(gameObject);
        }
    }
}
