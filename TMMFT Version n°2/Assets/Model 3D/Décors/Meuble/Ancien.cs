using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancien : MonoBehaviour {

	public bool TiroirDroit;

	private Vector3 PosInitial;
	private Vector3 PosFinal;

	public GameObject Meuble;

	private OuvrirTiroir ouvrirTiroir;

	public float variationX = -0.285f;
	public float variationY = +0.285f;

	// Use this for initialization
	void Start () {
		ouvrirTiroir = Meuble.GetComponent<OuvrirTiroir> ();

		PosInitial = gameObject.transform.position;
		PosFinal = new Vector3 (gameObject.transform.position.x + variationX, gameObject.transform.position.y, gameObject.transform.position.z + variationY);
	}

	// Update is called once per frame
	void Update () {

		if (ouvrirTiroir.EnAnimationDroit == true && TiroirDroit == true) {
			Vector3 newPos = ((PosFinal - PosInitial) * ouvrirTiroir.keyDroite / 100) + PosInitial;
			gameObject.transform.position = newPos;
		}

		if (ouvrirTiroir.EnAnimationGauche == true && TiroirDroit == false) {
			Vector3 newPos = ((PosFinal - PosInitial) * ouvrirTiroir.keyGauche / 100) + PosInitial;
			gameObject.transform.position = newPos;
		}
	}
}
