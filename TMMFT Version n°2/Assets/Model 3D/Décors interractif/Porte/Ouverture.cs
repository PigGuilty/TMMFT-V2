using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouverture : MonoBehaviour {

    private Animator animator;

    private int AnimationLength;
    private int AnimationWaitEnd;

    private float FloatDeLAnimation;

    public float FloatDeDépartPourAnimation;

    public BoxCollider boxOuvert;
    public BoxCollider boxFermé;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        AnimationLength = 30;
        AnimationWaitEnd = 0;

        animator.SetFloat("Ouverture", FloatDeDépartPourAnimation);

        boxOuvert.enabled = false;
        boxFermé.enabled = true;
    }

    public void HitByRaycast()
    {
        FloatDeLAnimation = animator.GetFloat("Ouverture");

        if (FloatDeLAnimation == 1f)
        {
                animator.Rebind();
                animator.SetFloat("Ouverture", 0.33f);
                boxOuvert.enabled = false;
                boxFermé.enabled = true;
        }

        if (FloatDeLAnimation == 0.33f)
        {
                animator.Rebind();
                animator.SetFloat("Ouverture", 1f);
                boxOuvert.enabled = true;
                boxFermé.enabled = false;
        }

    }
}
