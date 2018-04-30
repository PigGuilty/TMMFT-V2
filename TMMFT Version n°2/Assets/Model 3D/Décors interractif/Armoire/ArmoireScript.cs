using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ArmoireScript : MonoBehaviour {

    public GameObject Player;

    public GameObject SpawnTéléport;

    public bool Ouvert;
    private bool AnimationOuverture;

    private Animator animator;
    PortailUtilisable PortailUtilisablee;
    PrendreObjet prendreobjet;

    private int AnimationLengthOuverture;
    private int AnimationWaitEndOuverture;

    private int AnimationLengthTremblement;
    private int AnimationWaitEndTremblement;

    private int TempsPortailReload;

    private int TempsRandom;
    private int increase;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        PortailUtilisablee = Player.GetComponent<PortailUtilisable>();
        prendreobjet = Player.GetComponent<PrendreObjet>();

        AnimationOuverture = false;
        Ouvert = false;

        AnimationLengthOuverture = 50;
        AnimationWaitEndOuverture = 0;

        AnimationLengthTremblement = 60;
        AnimationWaitEndTremblement = 0;

        animator.SetFloat("Armoire", 0.0f);

        TempsRandom = Random.Range(120, 480);
    }


    // Update is called once per frame
    void Update () {

        if (AnimationOuverture == true)
        {
            if (AnimationWaitEndOuverture == 0)
            {
                animator.Rebind();
                animator.SetFloat("Armoire", 0.66f);

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
            }

            AnimationWaitEndOuverture = AnimationWaitEndOuverture + 1;

            if (AnimationWaitEndOuverture >= AnimationLengthOuverture)
            {
                Ouvert = true;
                AnimationOuverture = false;
                animator.SetFloat("Armoire", 1.0f);
            }
        }

        if (Ouvert == false)
        {
            if (increase < TempsRandom)
            {
                increase++;
            }

            if (increase >= TempsRandom)
            {
                if (AnimationWaitEndTremblement == 0)
                {
                    animator.Rebind();
                    animator.SetFloat("Armoire", 0.33f);
                }

                AnimationWaitEndTremblement = AnimationWaitEndTremblement + 1;

                if (AnimationWaitEndTremblement >= AnimationLengthTremblement)
                {
                    animator.SetFloat("Armoire", 0.0f);

                    AnimationWaitEndTremblement = 0;
                    increase = 0;
                    TempsRandom = Random.Range(120, 480);
                }
            }
        }
    }



    public void HitByRaycast()
    {
        if (Ouvert == false)
        {
            AnimationOuverture = true;
        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if (prendreobjet.ObjetPris == true && Ouvert == true && PortailUtilisablee.PortailUtilisablee == true && (other.tag == "Player" || other.tag == "Objet"))
        {
            PortailUtilisablee.PortailUtilisablee = false;
            TempsPortailReload = 0;
            Player.transform.position = SpawnTéléport.transform.position;
        }

        else if (Ouvert == true && PortailUtilisablee.PortailUtilisablee == true && (other.tag == "Player" || other.tag == "Objet"))
        {
            PortailUtilisablee.PortailUtilisablee = false;
            TempsPortailReload = 0;
            other.transform.position = SpawnTéléport.transform.position;
        }
    }


}