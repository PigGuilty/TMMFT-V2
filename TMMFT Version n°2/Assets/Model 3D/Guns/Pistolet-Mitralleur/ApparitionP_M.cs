﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionP_M : MonoBehaviour
{

    public Rigidbody rb;
    public Camera fpsCam;

    // Use this for initialization
    void Start()
    {
        float randomX = Random.Range(0.50f, 0.20f);
        float randomZ = Random.Range(0.50f, 0.20f);

        float finalX = fpsCam.transform.right.x + randomX;
        float finalZ = fpsCam.transform.right.z + randomZ;

        Vector3 rightForce = new Vector3(finalX, Random.Range(0.1f, 1f), finalZ);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(rightForce * Random.Range(20f, 200f));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
