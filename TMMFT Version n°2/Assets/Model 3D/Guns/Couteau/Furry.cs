using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furry : MonoBehaviour {

    private Attaque attaque;
    private Baguette baguette;

    private Animator animator;

    private int AnimationLengthCouteau;
    private int AnimationLengthBaguette;
    private int AnimationWaitEnd;

    private bool AnimCouteauVersBaguette;
    private bool AnimBaguetteVersCouteau;

    private void Awake()
    {
        attaque = GetComponent<Attaque>();
        baguette = GetComponent<Baguette>();
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        AnimCouteauVersBaguette = false;
        AnimBaguetteVersCouteau = false;
        AnimationLengthCouteau = 162;
        AnimationLengthBaguette = 200;
        AnimationWaitEnd = 0;
        baguette.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attaque.furry >= 100)
        {
            attaque.enabled = false;
            AnimCouteauVersBaguette = true;
            if (AnimCouteauVersBaguette == true)
            {
                if (AnimationWaitEnd == 0)
                {
                    animator.Rebind();
                    animator.SetFloat("Attaque Couteau", 0.5f);
                }

                AnimationWaitEnd = AnimationWaitEnd + 1;

                //attendre fin de l'annimation
                if (AnimationWaitEnd >= AnimationLengthCouteau)
                {
                    animator.SetFloat("Attaque Couteau", 0.75f);
                    AnimationWaitEnd = 0;
                    baguette.enabled = true;
                    AnimCouteauVersBaguette = false;
                    attaque.furry = 99;
                }

            }
        }
        if (attaque.furry <= -1)
        {
            baguette.enabled = false;
            AnimBaguetteVersCouteau = true;
            if (AnimBaguetteVersCouteau == true)
            {
                if (AnimationWaitEnd == 0)
                {
                    animator.Rebind();
                    animator.SetFloat("Attaque Couteau", 1f);
                }

                AnimationWaitEnd = AnimationWaitEnd + 1;

                //attendre fin de l'annimation
                if (AnimationWaitEnd >= AnimationLengthBaguette)
                {
                    animator.SetFloat("Attaque Couteau", 0f);
                    AnimationWaitEnd = 0;
                    attaque.enabled = true;
                    AnimBaguetteVersCouteau = false;
                    attaque.furry = 0;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            attaque.furry = attaque.furry - 5;
        }
     }
}
