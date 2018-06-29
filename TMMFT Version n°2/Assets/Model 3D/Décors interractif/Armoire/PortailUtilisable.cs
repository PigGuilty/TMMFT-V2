using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PortailUtilisable : NetworkBehaviour {

    public bool PortailUtilisablee;
    private float TempsPortailReload;

    // Use this for initialization
    void Start () {
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
		
        PortailUtilisablee = true;
        TempsPortailReload = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
		
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
