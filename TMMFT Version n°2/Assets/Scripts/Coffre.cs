using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour {

	public GameObject obj;
	
	void Ouvrir(){
		obj.GetComponent<Animator>().Rebind();
		obj.GetComponent<Animator>().SetFloat("Blend", 1.0f);
		obj.GetComponent<Collider>().enabled = false;
	}
}
