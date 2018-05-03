using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetGrenade : MonoBehaviour {

    public GameObject Explosion;

    public GameObject grenade;
    public GameObject Player;
    private PrendreObjet prendreobjet;

    public bool canExplode;

    // Use this for initialization
    void Start () {
        canExplode = false;
        prendreobjet = Player.GetComponent<PrendreObjet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(prendreobjet.ObjetPris == false)
        {
            canExplode = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canExplode == true)
        {
			if (other.gameObject.tag != "Player") {
				Instantiate(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
				Destroy(gameObject);
			}
        }
    }
}
