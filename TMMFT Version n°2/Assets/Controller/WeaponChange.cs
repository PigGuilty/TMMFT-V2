using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour {

	public GameObject pistolet;
	public GameObject fusil;
	public GameObject mitrailleur1;
	public GameObject mitrailleur2;
    public GameObject bazooka;

	// Use this for initialization
	void Start () {
		pistolet.SetActive(true);
		bazooka.SetActive(false);
		fusil.SetActive(false);
		mitrailleur1.SetActive(false);
		mitrailleur2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3) | Input.GetKeyDown (KeyCode.Alpha4)) {
			pistolet.SetActive(false);
			fusil.SetActive(false);
			mitrailleur1.SetActive(false);
			mitrailleur2.SetActive(false);
			bazooka.SetActive(false);
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			pistolet.SetActive(true);
		}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			fusil.SetActive(true);
		}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			mitrailleur1.SetActive(true);
			mitrailleur2.SetActive(true);
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			bazooka.SetActive(true);
		}
	}
}
