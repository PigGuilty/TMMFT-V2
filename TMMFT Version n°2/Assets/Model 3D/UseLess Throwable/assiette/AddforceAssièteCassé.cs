using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddforceAssièteCassé : MonoBehaviour {

    public assièteBreking assièteBreking;

    private int RandomNumber;
    private int increase;

    // Use this for initialization
    void Start () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        assièteBreking.GetComponent<assièteBreking>();
        print("assiète" + assièteBreking.dir);
        rb.AddForce(assièteBreking.dir * 500);

        RandomNumber = Random.Range(700, 740);
    }

    private void Update()
    {
        if(increase >= RandomNumber)
        {
            Destroy(gameObject);
        }

        increase++;
    }
}
