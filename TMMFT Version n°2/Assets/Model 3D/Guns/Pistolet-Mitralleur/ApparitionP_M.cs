using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionP_M : MonoBehaviour
{

    private Rigidbody rb;
	public bool right;
	public GameObject camToFollow;
	
	private bool done;

    // Use this for initialization
    void Update()
    {
		if(!done && camToFollow != null){
			float randomX = Random.Range(0.50f, 0.20f);
			float randomZ = Random.Range(0.50f, 0.20f);
			
			float finalX = 0;
			float finalZ = 0;
			
			if(Camera.main != null) {
				finalX = Camera.main.transform.right.x + randomX;
				finalZ = Camera.main.transform.right.z + randomZ;
			}else {
				finalX = randomX;
				finalZ = randomZ;	
			}
			
			if (right) {
				Vector3 rightForce = new Vector3 (finalX, Random.Range (0.1f, 1f), finalZ);

				rb = GetComponent<Rigidbody> ();
				rb.AddForce (rightForce * Random.Range (20f, 200f));
			} else {
				Vector3 rightForce = new Vector3 (finalX, Random.Range (0.1f, 1f), finalZ);

				rb = GetComponent<Rigidbody> ();
				rb.AddForce (rightForce * Random.Range (-20f, -200f));
			}
			done = true;
		}
    }
}
