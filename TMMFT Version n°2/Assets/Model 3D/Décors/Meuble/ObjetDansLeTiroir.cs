using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetDansLeTiroir : MonoBehaviour {

	public bool ObjectDansLeTriroirDroit;

	public Vector3 PosInitial;
	public Vector3 PosFinal;

	public GameObject Meuble;

	private OuvrirTiroir ouvrirTiroir;

	// Use this for initialization
	void Start () {
		ouvrirTiroir = Meuble.GetComponent<OuvrirTiroir> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (ouvrirTiroir.EnAnimationDroit == true && ObjectDansLeTriroirDroit == true) {
			Vector3 newPos = ((PosFinal - PosInitial) * ouvrirTiroir.keyDroite / 100) + PosInitial;
			gameObject.transform.position = newPos;
		}

		if (ouvrirTiroir.EnAnimationGauche == true && ObjectDansLeTriroirDroit == false) {
			Vector3 newPos = ((PosFinal - PosInitial) * ouvrirTiroir.keyGauche / 100) + PosInitial;
			gameObject.transform.position = newPos;
		}
	}
}
