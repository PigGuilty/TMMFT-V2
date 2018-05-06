﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Attaque : MonoBehaviour {

    public int TailleChargeur;
    private int BalleRestante;

    public float PortéeCouteau;
    public int DegatArmeCouteau;

    public float furry;
    private int furryMax;
    public int PointDeFurryGagnéParCoup;

    private Animator animator;

    private int AnimationLength;
    private int AnimationWaitEnd;

    public AudioClip SonAttaque;

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 45;
        AnimationWaitEnd = 0;
        furry = 0;
        furryMax = 100;
}

    // Update is called once per frame
    void Update () {
        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = SonAttaque;
                audio.Play();

                RaycastHit hit;
                Ray ShootingDirection = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                if(Physics.Raycast(ShootingDirection, out hit, PortéeCouteau)){

                    Target target = hit.transform.GetComponent<Target>();

                    if (hit.collider.tag == "Vache")
                    {
                        target.TakeDamage(DegatArmeCouteau,true);
                        furry = furry + PointDeFurryGagnéParCoup;
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * DegatArmeCouteau * 4);
                    }
                }
                
                BalleRestante = BalleRestante - 1;
            }
        }

        if (BalleRestante <= 0)
        {

            if (AnimationWaitEnd == 0)
            {
                animator.Rebind();
                animator.SetFloat("Attaque Couteau", 0.25f);
            }

            AnimationWaitEnd = AnimationWaitEnd + 1;

            //attendre fin de l'annimation
            if (AnimationWaitEnd >= AnimationLength)
            {
                animator.SetFloat("Attaque Couteau", 0f);
                BalleRestante = TailleChargeur;
                AnimationWaitEnd = 0;
            }

        }
    }
}
