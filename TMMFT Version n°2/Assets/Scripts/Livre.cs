using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livre : MonoBehaviour {

	public Material ID1;
	public Material ID2;
	public Material ID3;
	public Material ID4;
	public Material ID5;
	public Material ID6;
	public Material ID7;
	public Material ID8;
	public Material ID9;	
	public Material ID10;
	
	void Start () {
		int idColor = Random.Range(1,10);
		
		Material[] mats;
		switch (idColor){
			case 10 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID1;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 9 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID2;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 8 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID3;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 7 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID4;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 6 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID5;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 5 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID6;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 4 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID7;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 3 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID8;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 2 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID9;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
			case 1 :
				mats = gameObject.GetComponent<MeshRenderer>().materials;
                mats[0] = ID10;
                gameObject.GetComponent<MeshRenderer>().materials = mats;
                break;
		}
	}
}
