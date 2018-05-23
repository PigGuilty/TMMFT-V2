using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Ouverture : MonoBehaviour {

    private Animator animator;

    private float FloatDeLAnimation;

    public float FloatDeDépartPourAnimation;

    public BoxCollider boxOuvert;
    public BoxCollider boxFermé;

    private bool EnAnimation;

    private float AnimationWaitEnd;
    private float AnimationLength;

    public AudioClip SonOuverture;
    public AudioClip SonFermeture;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        animator.SetFloat("Ouverture", FloatDeDépartPourAnimation);

        boxOuvert.enabled = false;
        boxFermé.enabled = true;

        EnAnimation = false;
        AnimationWaitEnd = 0;
        AnimationLength = 0.58f;
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

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = SonFermeture;
                audio.Play();
            }

            if (FloatDeLAnimation == 0.33f)
            {
                animator.Rebind();
                animator.SetFloat("Ouverture", 1f);

                boxOuvert.enabled = true;
                boxFermé.enabled = false;

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = SonOuverture;
                audio.Play();
            }
        }
    }

    private void Update()
    {
        if(EnAnimation == true)
        {
            AnimationWaitEnd += Time.deltaTime;

            if (AnimationWaitEnd >= AnimationLength)
            {
                AnimationWaitEnd = 0;
                EnAnimation = false;
            }
        }
    }
}
