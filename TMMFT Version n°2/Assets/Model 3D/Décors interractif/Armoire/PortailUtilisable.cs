using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortailUtilisable : MonoBehaviour {

    public bool PortailUtilisablee;
    private int TempsPortailReload;

    // Use this for initialization
    void Start () {
        PortailUtilisablee = true;
        TempsPortailReload = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(PortailUtilisablee == false)
        {
            if (TempsPortailReload >= 20)
            {
                PortailUtilisablee = true;
                TempsPortailReload = 0;
            }
            TempsPortailReload++;
        }

    }
}
