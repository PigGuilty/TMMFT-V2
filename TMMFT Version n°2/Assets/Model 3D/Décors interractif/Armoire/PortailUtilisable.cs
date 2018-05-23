using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortailUtilisable : MonoBehaviour {

    public bool PortailUtilisablee;
    private float TempsPortailReload;

    // Use this for initialization
    void Start () {
        PortailUtilisablee = true;
        TempsPortailReload = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(PortailUtilisablee == false)
        {
            if (TempsPortailReload >= 0.4f)
            {
                PortailUtilisablee = true;
                TempsPortailReload = 0;
            }
            TempsPortailReload += Time.deltaTime;
        }

    }
}
