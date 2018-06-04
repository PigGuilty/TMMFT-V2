using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetLanterne : MonoBehaviour {

	public GameObject FireZone;
	private Vector3 dir;

	private bool UneFois = false;

	public void Update()
	{
		if (gameObject.GetComponent<Collider>().isTrigger == true && gameObject.GetComponent<Collider>().enabled == true && UneFois == false) {
			dir = new Vector3(Camera.main.transform.forward.x, -1, Camera.main.transform.forward.z);
			UneFois = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Untagged" || other.tag == "Vache" || other.tag == "Objet" || other.tag == "Decor interractif" || other.tag == "Escalier +x" || other.tag == "Escalier -x" || other.tag == "Escalier +y" || other.tag == "Escalier -y" || other.tag == "Meuble")
		{
			if (other.tag == "Decor interractif")
			{ 
				ArmoireScript armoire_script = other.GetComponent<ArmoireScript>();

				if (armoire_script != null && armoire_script.Ouvert == false){
					
					/*****Effet Feu****/
					GameObject FireZoneInstanciate = Instantiate (FireZone);
					FireZoneInstanciate.transform.position = gameObject.transform.position;

					Vector3 newPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

					RaycastHit hit;
					Ray ShootingDirection = new Ray (newPos, dir);
					Debug.DrawRay (newPos, dir, Color.red, 2f);

					if (Physics.Raycast (ShootingDirection, out hit)) {
						FireZoneInstanciate.transform.rotation = Quaternion.LookRotation (hit.normal);
						FireZoneInstanciate.transform.Rotate (Vector3.right * 90);
					}
					Destroy (gameObject);
					/*****Effet Feu****/
				}
			}

			else{
				/*****Effet Feu****/
				GameObject FireZoneInstanciate = Instantiate (FireZone);
				FireZoneInstanciate.transform.position = gameObject.transform.position;

				Vector3 newPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

				RaycastHit hit;
				Ray ShootingDirection = new Ray (newPos, dir);
				Debug.DrawRay (newPos, dir, Color.red, 2f);

				if (Physics.Raycast (ShootingDirection, out hit)) {
					FireZoneInstanciate.transform.rotation = Quaternion.LookRotation (hit.normal);
					FireZoneInstanciate.transform.Rotate (Vector3.right * 90);
				}
				Destroy (gameObject);
				/*****Effet Feu****/
			}
		}
	}
}