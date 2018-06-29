using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparitiondouille : MonoBehaviour
{

    public Rigidbody rb;
	public GameObject camToFollow;
	
	private bool done;
    // Use this for initialization
    void Update()
    {
		if(!done && camToFollow != null){
			float randomX = Random.Range(0.05f, 0.20f);
			float randomZ = Random.Range(0.05f, 0.20f);
			
			float finalX = 0;
			float finalZ = 0;
			
			if(camToFollow != null) {
				finalX = camToFollow.transform.right.x + randomX;
				finalZ = camToFollow.transform.right.z + randomZ;
			}else {
				finalX = randomX;
				finalZ = randomZ;	
			}
			
			Vector3 rightForce = new Vector3(finalX, 0, finalZ);

			rb = GetComponent<Rigidbody>();
			rb.AddForce(rightForce * Random.Range(100f, 200f));
			done = true;
		}
    }
}
