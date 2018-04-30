using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceSansSon : MonoBehaviour {

    public assièteBreking assièteBreking;

    private int RandomNumber;
    private int increase;

    private int NombreDeFoisQuilAJoueLeSon;

    // Use this for initialization
    void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        assièteBreking.GetComponent<assièteBreking>();

        rb.AddForce(assièteBreking.dir * 500);

        RandomNumber = Random.Range(700, 740);
    }

    private void Update()
    {
        if (increase >= RandomNumber)
        {
            Destroy(gameObject);
        }

        increase++;
    }
}
