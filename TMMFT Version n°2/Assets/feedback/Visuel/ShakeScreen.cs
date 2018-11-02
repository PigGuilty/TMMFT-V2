using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour {

	private Vector3 orgLocalPos;
	public float intensity;
	public bool shake;

	void Update () {
		if (shake) {
			transform.position += Random.insideUnitSphere * intensity;
		}
	}

	public void Shake() {
		orgLocalPos = transform.localPosition;
		shake = true;
	}

	public void Stop() {
		transform.localPosition = orgLocalPos;
		shake = false;
	}
}
