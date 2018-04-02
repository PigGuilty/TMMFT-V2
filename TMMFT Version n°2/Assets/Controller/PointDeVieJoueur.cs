﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDeVieJoueur : MonoBehaviour {

    public int PVMax;
    private int PV;
    public int DegatReçus;
    public int PVReçus;

    public GameObject SpawnQuandMort;
    public GameObject Canvas;

    public GameObject pistolet;
    public GameObject fusil;
    public GameObject mitrailleur1;
    public GameObject mitrailleur2;
    public GameObject bazooka;
    public GameObject Couteau;

    // Use this for initialization
    void Start () {
        PV = PVMax;
	}
	
	// Update is called once per frame
	void Update () {
        if ( PV <= 0)
        {
            meurt();
        }

        if (PV > PVMax)
        {
            PV = PVMax;
        }

        print(PV);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vache")
        {
            PV = PV - DegatReçus;
        }

        if (other.gameObject.tag == "Heal")
        {
            PV = PV + PVReçus;
        }
    }

    void meurt()
    {
        transform.position = SpawnQuandMort.transform.position;

        Canvas.SetActive(false);
        pistolet.SetActive(false);
        fusil.SetActive(false);
        mitrailleur1.SetActive(false);
        mitrailleur2.SetActive(false);
        bazooka.SetActive(false);
        Couteau.SetActive(false);
    }
}
