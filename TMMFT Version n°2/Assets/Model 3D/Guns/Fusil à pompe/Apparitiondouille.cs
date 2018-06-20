using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparitiondouille : MonoBehaviour
{

    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        float randomX = Random.Range(0.05f, 0.20f);
        float randomZ = Random.Range(0.05f, 0.20f);
		
		float finalX = 0;
		float finalZ = 0;
		
		if(Camera.main != null) {
			finalX = Camera.main.transform.right.x + randomX;
			finalZ = Camera.main.transform.right.z + randomZ;
		}else {
			finalX = randomX;
			finalZ = randomZ;	
		}
		
		Vector3 rightForce = new Vector3(finalX, 0, finalZ);

        rb = GetComponent<Rigidbody>();
        rb.AddForce(rightForce * Random.Range(100f, 200f));

    }
}
