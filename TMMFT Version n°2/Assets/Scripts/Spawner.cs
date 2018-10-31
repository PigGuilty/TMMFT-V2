using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Spawner : MonoBehaviour {

	public int SpawnSpeed;
	public GameObject EnnemiPrefab;
	private GameObject Player;
	private int Counter;
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;

	// Use this for initialization
	void Start () {
		Counter = SpawnSpeed+1;
		
		Player = GameObject.FindWithTag("localPlayer");
		if(Player != null){
			netSpawner = Player.GetComponent<NetworkSpawner>();
			m_isServer = Player.GetComponent<FirstPersonController>().isServer;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(EnnemiPrefab != null){
			if(Player == null){
				Player = GameObject.FindWithTag("localPlayer");
				if(Player != null){
					netSpawner = Player.GetComponent<NetworkSpawner>();
					m_isServer = Player.GetComponent<FirstPersonController>().isServer;
				}
			}
			
			Counter--;

			if (Counter <= 0) {
				Counter = SpawnSpeed;
				
				if(m_isServer){
					GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
					EnnemiPrefabInstanciate.transform.position = transform.position;
					NetworkServer.Spawn(EnnemiPrefabInstanciate);
				}else{
					//netSpawner.Spawn(EnnemiPrefab, transform.position, Quaternion.identity, -1, "");
				}	
				
			}
		}
	}
}
