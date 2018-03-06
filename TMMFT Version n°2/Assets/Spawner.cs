using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public int SpawnSpeed;
	public GameObject EnnemiPrefab;
	private int Counter;

	// Use this for initialization
	void Start () {
		Counter = SpawnSpeed+1;
	}
	
	// Update is called once per frame
	void Update () {
		Counter--;

		if (Counter <= 0) {
			Counter = SpawnSpeed;

			Instantiate(EnnemiPrefab, transform);
		}
	}
}
