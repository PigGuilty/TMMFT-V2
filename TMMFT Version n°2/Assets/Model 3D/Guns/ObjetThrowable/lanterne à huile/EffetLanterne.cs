using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetLanterne : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Effet De la lampe genre mettre du feu un peu partout
        print("Effet De la lampe genre mettre du feu un peu partout");
    }
}
