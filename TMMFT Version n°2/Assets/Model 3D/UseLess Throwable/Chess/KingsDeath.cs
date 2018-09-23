using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsDeath : MonoBehaviour {

	public GameObject Tresor;
	public GameObject kingsdeath;

	void OnCollisionEnter (Collision collision) {
		if (collision.transform.tag == "Bullet") {
			Tresor.SetActive (true);
			Destroy (kingsdeath.GetComponent<KingsDeath> ());
			Destroy (kingsdeath.GetComponent<Rigidbody> ());
		}
	}
}
