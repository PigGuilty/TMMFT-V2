using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TireLaser : MonoBehaviour {

	// Use this for initialization
	private Camera fpsCam;

	public AudioClip Bziou;

	void Start() {
		fpsCam = Camera.main;
	}

	// Update is called once per frame
	void Update () {

		Vector3 lookRot = fpsCam.transform.forward;

		if (Input.GetButtonDown("Fire1"))
		{

			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.clip = Bziou;
			audio.Play();

			RaycastHit hit;
			Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

			if (Physics.Raycast(ShootingDirection, out hit))
			{

				if (hit.collider.tag == "Decor interractif")
				{
					hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.transform.tag != "Player" && hit.transform.tag != "Vache" && hit.transform.tag != "Decor interractif" && hit.rigidbody != null)
				{
					hit.rigidbody.useGravity = !hit.rigidbody.useGravity;
					hit.rigidbody.AddForce(Vector3.up *3);
				}
			}
		}
	}
}
