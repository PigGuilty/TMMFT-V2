using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDeLaTete : MonoBehaviour {

	private Camera player;
	public GameObject Root_HeadBone;
	private Vector3 TargetPoint;

	// Use this for initialization
	void Start () {
		player = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		TargetPoint = new Vector3 (player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
		Root_HeadBone.transform.LookAt (player.transform.position ,Vector3.up);
	}
}
