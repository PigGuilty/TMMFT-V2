using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetGrenade : MonoBehaviour {

    public GameObject DemiSphère;
    public GameObject Sphère;

    public GameObject grenade;

    public bool canExplode;

    // Use this for initialization
    void start () {
        canExplode = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        canExplode = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canExplode == true)
        {
            if (collision.gameObject.tag != "Player")
            {
                if (collision.gameObject.tag == "Vache")
                {
                    Instantiate(Sphère, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                }
                else
                {
                    Instantiate(DemiSphère, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                }
                Destroy(gameObject);
            }
        }
    }
}
