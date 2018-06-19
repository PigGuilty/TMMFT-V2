using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoleDuMissile : MonoBehaviour {

    public ParticleSystem EffetFusée;
    public GameObject Explosion;

    private ArmoireScript armoirescript;

    // Use this for initialization
    void Start () {
        EffetFusée.Play();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-Vector3.forward * Time.deltaTime * 20);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Player") {
            if (other.gameObject.GetComponent<ArmoireScript>() != null)
            {
                armoirescript = other.gameObject.GetComponent<ArmoireScript>();

                if (armoirescript.Ouvert == false)
                {
                    Instantiate(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
                    Destroy(gameObject);
                }
            }
            else
            {
                Instantiate(Explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
                Destroy(gameObject);
            }
        }
    }
}
