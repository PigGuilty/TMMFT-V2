using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Spawner : MonoBehaviour {

	public int[] probas;
	public bool isEntered;

	public GameObject[] EnnemiPrefab;
	
	private GameObject Player;
	private bool m_isServer;
	private NetworkSpawner netSpawner;
	
	public void Call () {
		if(Player == null){
			Player = GameObject.FindWithTag("localPlayer");
			if(Player != null){
				netSpawner = Player.GetComponent<NetworkSpawner>();
				m_isServer = Player.GetComponent<FirstPersonController>().isServer;
			}
		}
		
		if(m_isServer){
			int random = Random.Range(0,100);
			
			if(random < probas[0]){
				GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab[0], transform);
				EnnemiPrefabInstanciate.transform.position = transform.position;
				NetworkServer.Spawn(EnnemiPrefabInstanciate);
			}else if(random < probas[1] + probas[0]){
				GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab[1], transform);
				EnnemiPrefabInstanciate.transform.position = transform.position;
				NetworkServer.Spawn(EnnemiPrefabInstanciate);
			}else if(random < probas[2] + probas[1] + probas[0]){
				GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab[2], transform);
				EnnemiPrefabInstanciate.transform.position = transform.position;
				NetworkServer.Spawn(EnnemiPrefabInstanciate);
			}
		}
	}
	
	private void OnTriggerStay(Collider other)
    {
		if(other.tag == "Player" || other.tag == "localPlayer"){
			isEntered = true;
		}
	}
	
	private void OnTriggerExit(Collider other)
    {
		if(other.tag == "Player" || other.tag == "localPlayer"){
			isEntered = false;
		}
	}
}
