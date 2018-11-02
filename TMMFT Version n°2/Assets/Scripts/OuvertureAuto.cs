using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvertureAuto : MonoBehaviour {

    public BoxCollider boxOuvert;
    public BoxCollider boxFermé;
	
	private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Vache" && gameObject.transform.parent.gameObject.GetComponent<Animator>().GetFloat("Ouverture") == 0.33f){
			gameObject.transform.parent.gameObject.GetComponent<Animator>().Rebind();
			gameObject.transform.parent.gameObject.GetComponent<Animator>().SetFloat("Ouverture", 1f);
			boxOuvert.enabled = true;
            boxFermé.enabled = false;
		}
	}
}
