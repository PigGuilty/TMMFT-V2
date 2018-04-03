using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette : MonoBehaviour {

    private Attaque attaque;

    private int ok;

    private void Awake()
    {
        attaque = GetComponent<Attaque>();
        ok = 2;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (ok >= 2)
        {
            attaque.furry = attaque.furry - 1;
            ok = 0;
        }
        if (ok < 2)
        {
            ok = ok + 1;
        }

    }
}
