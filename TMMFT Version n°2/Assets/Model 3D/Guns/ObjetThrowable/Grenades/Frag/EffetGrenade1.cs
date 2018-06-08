﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetGrenade1 : MonoBehaviour {

    public GameObject Explosion;

    private GameObject Player;
    private PrendreObjet prendreobjet;

    public bool canExplode;
    private bool DébutDeLAction;

    private float timer;
	private float randNumb;

    public BoxCollider boxTrigger;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player");

        DébutDeLAction = false;
        canExplode = false;
        prendreobjet = Player.GetComponent<PrendreObjet>();

		randNumb = Random.Range (2.5f, 3.0f);
    }

    private void Update()
    {
        if (boxTrigger.enabled == true)
        {
            DébutDeLAction = true;
        }

        if (prendreobjet.ObjetPris == false && DébutDeLAction == true)
        {
            canExplode = true;
            gameObject.tag = "Untagged"; 
        }

        if (canExplode == true)
        {
			if(timer >= randNumb)
            {
                GameObject Explo = Instantiate(Explosion);
                Explo.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
            timer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canExplode == true)
        {
			if (other.gameObject.tag == "Bullet") {
                GameObject Explo = Instantiate(Explosion);
                Explo.transform.position = gameObject.transform.position;
                Destroy(gameObject);
			}
        }
    }
}
