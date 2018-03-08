using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoleDuMissile : MonoBehaviour {

    public ParticleSystem EffetFusée;
    public GameObject Sphère;

	// Use this for initialization
	void Start () {
        EffetFusée.Play();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-Vector3.forward * Time.deltaTime * 20);
    }

    void OnTriggerEnter(Collider collision)
    {
        Instantiate(Sphère, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(gameObject);
    }

}
