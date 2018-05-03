using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette : MonoBehaviour {

	public GameObject LASER;
	public GameObject player;

    private Attaque attaque;
    private float ok;

	private void Awake() {
		attaque = GetComponent<Attaque> ();
		ok = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {

		if (ok >= 0.05f && Input.GetButton ("Fire1"))
        {
            attaque.furry = attaque.furry - 1;
            ok = 0;
        }
		if (ok < 0.05f && Input.GetButton ("Fire1"))
        {
			ok += Time.deltaTime;
        }

		if (Input.GetButton ("Fire1")) {
			player.transform.SendMessage ("Shake", SendMessageOptions.DontRequireReceiver);
			LASER.SetActive (true);
		}

		if (Input.GetButtonUp ("Fire1")) {
			player.transform.SendMessage ("Stop", SendMessageOptions.DontRequireReceiver);
			LASER.SetActive (false);
		}
    }

	void OnDisable(){
		player.transform.SendMessage ("Stop", SendMessageOptions.DontRequireReceiver);
		LASER.SetActive (false);
	}
}
