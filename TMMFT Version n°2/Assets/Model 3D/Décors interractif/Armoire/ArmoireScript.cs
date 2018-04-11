using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoireScript : MonoBehaviour {

    public GameObject Player;

    public GameObject SpawnTéléport;

    private bool Ouvert;
    private bool AnimationOuverture;

    private Animator animator;
    PortailUtilisable PortailUtilisablee;

    private int AnimationLengthOuverture;
    private int AnimationWaitEndOuverture;

    private int TempsPortailReload;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        PortailUtilisablee = Player.GetComponent<PortailUtilisable>();

        AnimationOuverture = false;
        Ouvert = false;

        AnimationLengthOuverture = 50;
        AnimationWaitEndOuverture = 0;

        animator.SetFloat("Armoire", 0.0f);
    }


    // Update is called once per frame
    void Update () {

        if (AnimationOuverture == true)
        {
            if (AnimationWaitEndOuverture == 0)
            {
                animator.Rebind();
                animator.SetFloat("Armoire", 0.66f);
            }

            AnimationWaitEndOuverture = AnimationWaitEndOuverture + 1;

            if (AnimationWaitEndOuverture >= AnimationLengthOuverture)
            {
                Ouvert = true;
                AnimationOuverture = false;
                animator.SetFloat("Armoire", 1.0f);
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
        if (Ouvert == true && PortailUtilisablee.PortailUtilisablee == true && (other.tag == "Player" || other.tag == "Objet"))
        {
            PortailUtilisablee.PortailUtilisablee = false;
            TempsPortailReload = 0;
            other.transform.position = SpawnTéléport.transform.position;
        }
    }


}