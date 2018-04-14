using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MédikitDéPop : MonoBehaviour {

    public GameObject Médikit;
    public GameObject Player;

    private PointDeVieJoueur PVJoueurClass;
    private int PVJoueur;

    // Use this for initialization
    void Start () {
        PVJoueurClass = Player.GetComponent<PointDeVieJoueur>();
    }

    private void Update()
    {
        PVJoueur = PVJoueurClass.PV;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PVJoueur < 100)
        {
            Médikit.SetActive(false);
        }
    }
}