using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetFrag : MonoBehaviour {

	public int NombreDeGrenades;

	public GameObject FragmentGrenade;
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
        prendreobjet = Player.GetComponent<PrendreObjet>();
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
            if(timer >= 3)
            {
				for (int i = 0; i < NombreDeGrenades; i++){
					GameObject FragmentGrenadeInstantiate = Instantiate(FragmentGrenade);
					FragmentGrenadeInstantiate.transform.position = gameObject.transform.position + new Vector3(Random.Range(-0.35f,0.35f), Random.Range(-0.35f,0.35f), Random.Range(-0.35f,0.35f));
				}

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
			if (other.gameObject.tag == "Bullet")
			{
				for (int i = 0; i < NombreDeGrenades; i++){
					GameObject FragmentGrenadeInstantiate = Instantiate(FragmentGrenade);
					FragmentGrenadeInstantiate.transform.position = gameObject.transform.position;
				}
				GameObject Explo = Instantiate(Explosion);
				Explo.transform.position = gameObject.transform.position;
				Destroy(gameObject);
			}
        }
    }
}
