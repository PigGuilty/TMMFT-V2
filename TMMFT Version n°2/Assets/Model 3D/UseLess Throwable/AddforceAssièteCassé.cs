using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddforceAssièteCassé : MonoBehaviour {

    public assièteBreking assièteBreking;

	// Use this for initialization
	void Start () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        assièteBreking.GetComponent<assièteBreking>();
        print("assiète" + assièteBreking.dir);
        rb.AddForce(assièteBreking.dir * 500);
	}
}
