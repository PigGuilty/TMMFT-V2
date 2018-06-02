using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvrirTiroir : MonoBehaviour {

	private float keyGauche = 0.0f;
	private float keyDroite = 0.0f;

	private bool GaucheOuvert = false;
	private bool DroiteOuvert = false;

	public BoxCollider BoxGauche;
	public BoxCollider BoxDroite;

	public void OuvrirGauche()
	{
		print ("Gauche");
		InvokeRepeating ("Gauche", 0.0f, 0.005f);
	}

	public void OuvrirDroite()
	{
		print ("Drote");
		InvokeRepeating ("Droite", 0.0f, 0.005f);
	}

	void Gauche(){
		if (GaucheOuvert == false) {

			if (keyGauche >= 100.0f) {
				
				GaucheOuvert = true;
				BoxGauche.enabled = true;
				CancelInvoke ("Gauche");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, keyGauche++);
		}

		if (GaucheOuvert == true) {

			if (keyGauche <= 0.0f) {
				
				GaucheOuvert = false;
				BoxGauche.enabled = false;
				CancelInvoke ("Gauche");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, keyGauche--);
		}
	}

	void Droite(){
		if (DroiteOuvert == false) {

			if (keyDroite >= 100.0f) {
				
				DroiteOuvert = true;
				BoxDroite.enabled = true;
				CancelInvoke ("Droite");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, keyDroite++);
		}

		if (DroiteOuvert == true) {

			if (keyDroite <= 0.0f) {
				
				DroiteOuvert = false;
				BoxDroite.enabled = false;
				CancelInvoke ("Droite");
			}

			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, keyDroite--);
		}
	}
}
