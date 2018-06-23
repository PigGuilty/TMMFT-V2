using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerByButtun : MonoBehaviour {

    public GameObject EnnemiPrefab;
	public int NombreDeMobSpawn;

    public void Appuyé()
    {
		for(int i = 0; i < NombreDeMobSpawn; i++){
        	GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
        	EnnemiPrefabInstanciate.transform.position = transform.position;
		}

    }

    public void Relaché()
    {
		for(int i = 0; i < NombreDeMobSpawn; i++){
			GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
			EnnemiPrefabInstanciate.transform.position = transform.position;
		}
		if (NombreDeMobSpawn >= 5) {
			NombreDeMobSpawn += NombreDeMobSpawn;
		}
    }
}
