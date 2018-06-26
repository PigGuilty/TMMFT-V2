using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class SpawnerByButtun : MonoBehaviour {

    public GameObject EnnemiPrefab;
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
	private GameObject Player;
	public int NombreDeMobSpawn;
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
        if(m_isServer){
			GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
			EnnemiPrefabInstanciate.transform.position = transform.position;
			NetworkServer.Spawn(EnnemiPrefabInstanciate);
		}else{
			netSpawner.Spawn(EnnemiPrefab, transform.position, Quaternion.identity, -1, "");
		}
    }

    public void Relaché()
    {
		
		for(int i = 0; i < NombreDeMobSpawn; i++){
			if(m_isServer){
				GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
				EnnemiPrefabInstanciate.transform.position = transform.position;
				NetworkServer.Spawn(EnnemiPrefabInstanciate);
			}else{
				netSpawner.Spawn(EnnemiPrefab, transform.position, Quaternion.identity, -1, "");
			}	
		}
		if (NombreDeMobSpawn >= 5) {
			NombreDeMobSpawn += NombreDeMobSpawn;
		}
    }
}
