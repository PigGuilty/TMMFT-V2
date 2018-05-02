using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDownEscalier : MonoBehaviour {

    private Rigidbody rb;

    public float x;
    public float y;
    public float z;
    public float speed;

  
/*    void Start () {
		
	}
	

	void Update () {
		
	}*/

    private void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        if (other.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(x, y, z) * speed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        if (other.gameObject.tag == "Player")
        {
            rb.AddForce(new Vector3(x, y, z) * speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.AddForce(new Vector3(x, -y, z) * speed * 200);
            rb.isKinematic = true;
        }
    }
}
