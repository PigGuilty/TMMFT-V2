using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OuvrirTiroir : NetworkBehaviour {

	[SyncVar]
	public float keyGauche = 0.0f;
	[SyncVar]
	public float keyDroite = 0.0f;

	[SyncVar]
	private bool GaucheOuvert = false;
	[SyncVar]
	private bool DroiteOuvert = false;

	public BoxCollider BoxGauche;
	public BoxCollider BoxDroite;

	private int VitesseAnimation = 60;

	[SyncVar]
	public bool EnAnimationDroit = false;
	[SyncVar]
	public bool EnAnimationGauche = false;

	public void OuvrirGauche()
	{
		EnAnimationGauche = true;
		InvokeRepeating ("Gauche", 0.0f, 0.005f);
	}

	public void OuvrirDroite()
	{
		EnAnimationDroit = true;
		InvokeRepeating ("Droite", 0.0f, 0.005f);
	}

	void Gauche(){
		if (GaucheOuvert == false) {

			if (keyGauche >= 100.0f) {
				
				GaucheOuvert = true;
				BoxGauche.enabled = true;
				EnAnimationGauche = false;
				CancelInvoke ("Gauche");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, keyGauche+= Time.deltaTime * VitesseAnimation);
		}

		if (GaucheOuvert == true) {

			if (keyGauche <= 0.0f) {
				
				GaucheOuvert = false;
				BoxGauche.enabled = false;
				EnAnimationGauche = false;
				CancelInvoke ("Gauche");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, keyGauche-= Time.deltaTime * VitesseAnimation);
		}
	}

	void Droite(){
		if (DroiteOuvert == false) {

			if (keyDroite >= 100.0f) {
				
				DroiteOuvert = true;
				BoxDroite.enabled = true;
				EnAnimationDroit = false;
				CancelInvoke ("Droite");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, keyDroite+= Time.deltaTime * VitesseAnimation);
		}

		if (DroiteOuvert == true) {

			if (keyDroite <= 0.0f) {
				
				DroiteOuvert = false;
				BoxDroite.enabled = false;
				EnAnimationDroit = false;
				CancelInvoke ("Droite");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, keyDroite-= Time.deltaTime * VitesseAnimation);
		}
	}
}
