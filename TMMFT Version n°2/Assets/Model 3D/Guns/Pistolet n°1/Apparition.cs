using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparition : MonoBehaviour {

    public Rigidbody rb;
	public GameObject camToFollow;
	private bool done;
	
    void Update () {
		if(!done && camToFollow != null){
			float randomX = Random.Range(-0.15f, 0.15f);
			float randomZ = Random.Range(-0.15f, 0.15f);

			float finalX = camToFollow.transform.forward.x + randomX;
			float finalZ = camToFollow.transform.forward.z + randomZ;

			Vector3 forwardForce = new Vector3(finalX, -camToFollow.transform.forward.y, finalZ);

			rb = GetComponent<Rigidbody>();
			rb.AddForce(forwardForce * Random.Range(200f, 400f));
			
			done = true;
		}
    }
}
