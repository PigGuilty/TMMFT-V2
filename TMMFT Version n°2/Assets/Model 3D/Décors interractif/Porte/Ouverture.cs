﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouverture : MonoBehaviour {

    private Animator animator;

    private float FloatDeLAnimation;

    public float FloatDeDépartPourAnimation;

    public BoxCollider boxOuvert;
    public BoxCollider boxFermé;

    private bool EnAnimation;

    private int AnimationWaitEnd;
    private int AnimationLength;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        animator.SetFloat("Ouverture", FloatDeDépartPourAnimation);

        boxOuvert.enabled = false;
        boxFermé.enabled = true;

        EnAnimation = false;
        AnimationWaitEnd = 0;
        AnimationLength = 30;
    }

    public void HitByRaycast()
    {
        if (EnAnimation == false)
        {

            FloatDeLAnimation = animator.GetFloat("Ouverture");

            EnAnimation = true;

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

    private void Update()
    {
        if(EnAnimation == true)
        {
            AnimationWaitEnd = AnimationWaitEnd + 1;

            if (AnimationWaitEnd >= AnimationLength)
            {
                AnimationWaitEnd = 0;
                EnAnimation = false;
            }
        }
    }
}
