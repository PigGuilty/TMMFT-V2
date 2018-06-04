using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisPrendFeu : MonoBehaviour {

	public GameObject FireZone;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Feu") {
			FireZone.SetActive (true);
			Destroy (gameObject, 15.0f);
		}
	}
}
