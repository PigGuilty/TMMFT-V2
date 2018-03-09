using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoleDuMissile : MonoBehaviour {

    public ParticleSystem EffetFusée;
    public GameObject DemiSphère;
    public GameObject Sphère;

    // Use this for initialization
    void Start () {
        EffetFusée.Play();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-Vector3.forward * Time.deltaTime * 20);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Vache")
            {
                Instantiate(Sphère, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
            }
            else
            {
                Instantiate(DemiSphère, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
            }
            Destroy(gameObject);
        }
    }

}
