using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MédikitDéPop : MonoBehaviour {

    public GameObject Médikit;
    private GameObject Player;

    private PointDeVieJoueur PVJoueurClass;
    private int PVJoueur;

    // Use this for initialization
    void Start () {
		Player = GameObject.FindWithTag("Player");
		if (Player != null)
			PVJoueurClass = Player.GetComponent<PointDeVieJoueur>();
    }

    private void Update()
    {
		if(Player == null){
			Player = GameObject.FindWithTag("Player");
			if (Player != null)
				PVJoueurClass = Player.GetComponent<PointDeVieJoueur>();
		}else{
			PVJoueur = PVJoueurClass.PV;
		}
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PVJoueur < 100)
        {
            Médikit.SetActive(false);
        }
    }
}