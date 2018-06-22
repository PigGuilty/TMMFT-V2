using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeKeyBipede : MonoBehaviour {

	public Target target;
	private SkinnedMeshRenderer skinRend;
	private Material Red;
	private Material[] mats;

	private bool DommageTaken = false;
	private float increaser = 0.0f;

	// Use this for initialization
	void Start () {
		skinRend = GetComponent<SkinnedMeshRenderer> ();
		mats = skinRend.materials;
		Red = target.Red;
	}
	
	// Update is called once per frame
	void Update () {
		print (DommageTaken);
		if (mats [0].color == Red.color) {
			DommageTaken = true;
			skinRend.SetBlendShapeWeight (0, 100.0f);
		}

		if (DommageTaken == true) {
			increaser += Time.deltaTime;
			if (increaser > 0.5f) {
				skinRend.SetBlendShapeWeight (0, 0.0f);
				increaser = 0.0f;
				DommageTaken = false;
			}
		}
		//print (Mathf.Abs ((target.Vie / target.VieMax * 100) - 100));
		skinRend.SetBlendShapeWeight (1, Mathf.Abs((target.Vie / target.VieMax * 100) - 100));
		skinRend.GetBlendShapeWeight (1);
	}
}
