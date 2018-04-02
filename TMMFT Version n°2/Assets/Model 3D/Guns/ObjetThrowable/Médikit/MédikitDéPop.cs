using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MédikitDéPop : MonoBehaviour {

    public GameObject Médikit;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(Médikit);
        }
    }
}