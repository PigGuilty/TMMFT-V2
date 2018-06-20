using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetGrenade4 : MonoBehaviour {

    public GameObject Explosion;

    private GameObject Player;
    private PrendreObjet prendreobjet;

    public bool canExplode;
    private bool DébutDeLAction;

    private float timer;
	
    public BoxCollider boxTrigger;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player");

        DébutDeLAction = false;
        canExplode = false;
		if(Player != null)
			prendreobjet = Player.GetComponent<PrendreObjet>();
    }

    private void Update()
    {
		if (Player == null){//update player
			Player = GameObject.FindWithTag("Player");
			if(Player != null)
				prendreobjet = Player.GetComponent<PrendreObjet>();
		}else{
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
				if(timer >= 3)
				{
					GameObject Explo = Instantiate(Explosion);
					Explo.transform.position = gameObject.transform.position;
					Destroy(gameObject);
				}
				timer += Time.deltaTime;
			}
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
