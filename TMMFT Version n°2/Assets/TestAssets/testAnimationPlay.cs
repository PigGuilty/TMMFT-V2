using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnimationPlay : MonoBehaviour {

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        bool tourne = Input.GetKey(KeyCode.Space);
       
        if (tourne == true)
        {
            animator.SetFloat("Test Avance", 1);
        }

	}
}
