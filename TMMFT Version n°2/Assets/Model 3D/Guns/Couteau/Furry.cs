using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furry : MonoBehaviour {

    private Attaque attaque;
    private Baguette baguette;

    private void Awake()
    {
        attaque = GetComponent<Attaque>();
        baguette = GetComponent<Baguette>();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (attaque.furry >= 100)
        {
            attaque.enabled = false;
            baguette.enabled = true;
        }
	}
}
