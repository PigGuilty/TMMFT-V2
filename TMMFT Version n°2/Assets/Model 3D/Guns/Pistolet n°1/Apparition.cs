using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparition : MonoBehaviour {

    public Rigidbody rb;
    public Camera fpsCam;

    // Use this for initialization
    void Start () {
        float randomX = Random.Range(-0.15f, 0.15f);
        float randomZ = Random.Range(-0.15f, 0.15f);

        float finalX = fpsCam.transform.forward.x + randomX;
        float finalZ = fpsCam.transform.forward.z + randomZ;

        Vector3 forwardForce = new Vector3(finalX, -fpsCam.transform.forward.y, finalZ);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(forwardForce * Random.Range(200f, 400f));

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
