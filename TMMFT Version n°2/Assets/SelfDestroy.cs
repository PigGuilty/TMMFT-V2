using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float DuréeDeVie;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, DuréeDeVie);
	}
}
