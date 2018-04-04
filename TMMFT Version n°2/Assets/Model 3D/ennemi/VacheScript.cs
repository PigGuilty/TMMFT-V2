using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacheScript : MonoBehaviour {

    Rigidbody rb;

    public float Speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 Newpos = Camera.main.transform.position - Vector3.up *3;

        Vector3 PlayerDirection = Newpos - transform.position;

        Quaternion rotation = Quaternion.LookRotation(PlayerDirection);
        transform.rotation = rotation;

        rb.AddForce(PlayerDirection * Speed);

    }
}
