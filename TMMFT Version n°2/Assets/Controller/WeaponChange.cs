using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour {

	public GameObject pistolet;
	public GameObject fusil;
	public GameObject mitrailleur;
	//public GameObject bazooka;

	// Use this for initialization
	void Start () {
		pistolet.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3) | Input.GetKeyDown (KeyCode.Alpha4)) {
			pistolet.SetActive(false);
			fusil.SetActive(false);
			mitrailleur.SetActive(false);
			//bazooka.enabled = false;
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			pistolet.SetActive(true);
		}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			fusil.SetActive(true);
		}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			mitrailleur.SetActive(true);
		}/* else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			bazooka.enabled = true;
		}*/
	}
}
