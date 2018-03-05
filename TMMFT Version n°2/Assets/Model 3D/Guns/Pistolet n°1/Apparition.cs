using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparition : MonoBehaviour {

    public Rigidbody rb;

    // Use this for initialization
    void Start () {
        float randomX = Random.Range(-0.15f, 0.15f);
        float randomZ = Random.Range(-0.15f, 0.15f);

        float finalX = Camera.main.transform.forward.x + randomX;
        float finalZ = Camera.main.transform.forward.z + randomZ;

        Vector3 forwardForce = new Vector3(finalX, -Camera.main.transform.forward.y, finalZ);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(forwardForce * Random.Range(200f, 400f));

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
