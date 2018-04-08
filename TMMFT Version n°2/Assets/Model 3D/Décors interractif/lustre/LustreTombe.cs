using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustreTombe : MonoBehaviour {

    public GameObject DemiSphère;
    public GameObject Sphère;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitByRaycast ()
    {
        rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Objet")
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
