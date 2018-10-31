using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]

public class TireLaser : NetworkBehaviour {

	private Transform fpsCam;

	public AudioClip Bziou;
	
	void Start() {
		if (gameObject.transform.parent.parent.tag != "localPlayer")
		{
			return;
		}
		fpsCam = gameObject.transform.parent;
	}

	void Update () {	
		if (gameObject.transform.parent.parent.tag != "localPlayer")
		{
			return;
		}
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
					hit.rigidbody.AddForce(Vector3.up);
				}
			}
		}
	}
}
