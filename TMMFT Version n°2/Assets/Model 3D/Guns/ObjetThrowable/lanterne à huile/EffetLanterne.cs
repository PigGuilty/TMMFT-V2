using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class EffetLanterne : MonoBehaviour {

	public GameObject FireZone;
	private Vector3 dir;

	private bool UneFois = false;
	public bool m_isServer;
	public bool m_isLocalPlayer;
	
	private NetworkSpawner netSpawner;
	private GameObject Player;
    private Camera fpsCam;

	void Start () {
        Player = GameObject.FindWithTag("localPlayer");
		if(Player != null){
			netSpawner = Player.GetComponent<NetworkSpawner>();
			m_isServer = Player.GetComponent<FirstPersonController>().isServer;
			fpsCam = GameObject.FindWithTag("localCamera").GetComponent<Camera>();
		}
	}
	
	public void Update()
	{
		if(Player == null){
			Player = GameObject.FindWithTag("localPlayer");
			if(Player != null){
				netSpawner = Player.GetComponent<NetworkSpawner>();
				m_isServer = Player.GetComponent<FirstPersonController>().isServer;
				fpsCam = GameObject.FindWithTag("localCamera").GetComponent<Camera>();
			}
		}
		
		if (gameObject.GetComponent<Collider>().isTrigger == true && gameObject.GetComponent<Collider>().enabled == true && UneFois == false) {
			dir = new Vector3(fpsCam.transform.forward.x, -1, fpsCam.transform.forward.z);
			UneFois = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Untagged" || other.tag == "Vache" || other.tag == "Objet" || other.tag == "Decor interractif" || other.tag == "Escalier +x" || other.tag == "Escalier -x" || other.tag == "Escalier +y" || other.tag == "Escalier -y" || other.tag == "Meuble")
		{
			if (other.tag == "Decor interractif")
			{ 
				ArmoireScript armoire_script = other.GetComponent<ArmoireScript>();

				if (armoire_script != null && armoire_script.Ouvert == false){
					
					/*****Effet Feu****/					
					Vector3 newPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

					RaycastHit hit;
					Ray ShootingDirection = new Ray (newPos, dir);
					Debug.DrawRay (newPos, dir, Color.red, 2f);
					Quaternion rot = Quaternion.identity;

					if (Physics.Raycast (ShootingDirection, out hit)) {
						rot = Quaternion.LookRotation (hit.normal);
						rot.x += 90;
					}
					
					if (m_isServer && m_isLocalPlayer){
						GameObject FireZoneInstanciate = Instantiate(FireZone, gameObject.transform.position, rot);
						NetworkServer.Spawn(FireZoneInstanciate);
					}else if (m_isLocalPlayer){
						netSpawner.Spawn(FireZone, gameObject.transform.position, rot, -1, "");
					}
					
					Destroy (gameObject);
					/*****Effet Feu****/
				}
			}

			else{
				/*****Effet Feu****/					
				Vector3 newPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

				RaycastHit hit;
				Ray ShootingDirection = new Ray (newPos, dir);
				Debug.DrawRay (newPos, dir, Color.red, 2f);
				Quaternion rot = Quaternion.identity;

				if (Physics.Raycast (ShootingDirection, out hit)) {
					rot = Quaternion.LookRotation (hit.normal);
					rot.x += 90;
				}
				
				if(m_isServer){
					GameObject FireZoneInstanciate = Instantiate(FireZone, gameObject.transform.position, rot);
					NetworkServer.Spawn(FireZoneInstanciate);
				}else{
					netSpawner.Spawn(FireZone, gameObject.transform.position, rot, -1, "");
				}
				
				Destroy (gameObject);
				/*****Effet Feu****/
			}
		}
	}
}