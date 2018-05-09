using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetLanterne : MonoBehaviour {

    public GameObject FireZone;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Effet De la lampe genre mettre du feu un peu partout
        GameObject FireZoneInstanciate = Instantiate(FireZone);
        FireZoneInstanciate.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
