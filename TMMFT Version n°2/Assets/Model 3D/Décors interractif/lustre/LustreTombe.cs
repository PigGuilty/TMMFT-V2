using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustreTombe : MonoBehaviour {

    public GameObject Explosion;

    Rigidbody rb;
	AudioSource audio;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource> ();
	}

    public void HitByRaycast ()
    {
        rb.isKinematic = false;
		audio.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Objet")
        {
			Instantiate(Explosion, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
			Destroy(gameObject);
        }
    }
}
