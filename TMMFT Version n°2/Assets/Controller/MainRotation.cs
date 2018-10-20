using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRotation : MonoBehaviour {

	private GameObject Camera;

	// Use this for initialization
	void Start () {
		Camera = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponentInChildren<Camera> ().gameObject;
		print (Camera);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Camera.transform.rotation;
		if (transform.rotation.x > 0.0f) {
			transform.localPosition = new Vector3(0.0f, 0.0027f  - (transform.rotation.x / 75.0f) , - (transform.rotation.x / 125.0f));
		}
	}
}
