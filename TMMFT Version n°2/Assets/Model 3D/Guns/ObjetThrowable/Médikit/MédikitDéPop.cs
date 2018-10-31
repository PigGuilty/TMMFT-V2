using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MédikitDéPop : MonoBehaviour {

    public GameObject Médikit;
	
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "localPlayer" || other.tag == "Player") && other.GetComponent<PointDeVieJoueur>().PV < 100)
        {
            Médikit.SetActive(false);
        }
    }
}