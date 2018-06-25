using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class SpawnerByButtun : MonoBehaviour {

    public GameObject EnnemiPrefab;
<<<<<<< HEAD
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
	private GameObject Player;
=======
	public int NombreDeMobSpawn;
>>>>>>> ee6977bfa153c9f8336b72b4a1b868ecddc38823

	void Start () {
        Player = GameObject.FindWithTag("localPlayer");
		if(Player != null){
			netSpawner = Player.GetComponent<NetworkSpawner>();
			m_isServer = Player.GetComponent<FirstPersonController>().isServer;
		}
	}
	void Update () {
		if(Player == null){
			Player = GameObject.FindWithTag("localPlayer");
			if(Player != null){
				netSpawner = Player.GetComponent<NetworkSpawner>();
				m_isServer = Player.GetComponent<FirstPersonController>().isServer;
			}
		}
	}
	
    public void Appuyé()
    {
<<<<<<< HEAD
        if(m_isServer){
			GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
			EnnemiPrefabInstanciate.transform.position = transform.position;
			NetworkServer.Spawn(EnnemiPrefabInstanciate);
		}else{
			netSpawner.Spawn(EnnemiPrefab, transform.position, Quaternion.identity, -1, "");
		}
=======
		for(int i = 0; i < NombreDeMobSpawn; i++){
        	GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
        	EnnemiPrefabInstanciate.transform.position = transform.position;
		}

>>>>>>> ee6977bfa153c9f8336b72b4a1b868ecddc38823
    }

    public void Relaché()
    {
<<<<<<< HEAD
		if(m_isServer){
			GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
			EnnemiPrefabInstanciate.transform.position = transform.position;
			NetworkServer.Spawn(EnnemiPrefabInstanciate);
		}else{
			netSpawner.Spawn(EnnemiPrefab, transform.position, Quaternion.identity, -1, "");
		}	
=======
		for(int i = 0; i < NombreDeMobSpawn; i++){
			GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
			EnnemiPrefabInstanciate.transform.position = transform.position;
		}
		if (NombreDeMobSpawn >= 5) {
			NombreDeMobSpawn += NombreDeMobSpawn;
		}
>>>>>>> ee6977bfa153c9f8336b72b4a1b868ecddc38823
    }
}
