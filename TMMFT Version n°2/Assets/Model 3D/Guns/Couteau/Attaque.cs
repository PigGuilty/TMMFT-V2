using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaque : MonoBehaviour {

    public int TailleChargeur;
    private int BalleRestante;

    public float PortéeCouteau;

    private Animator animator;

    private int AnimationLength;
    private int AnimationWaitEnd;

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 45;
        AnimationWaitEnd = 0;
    }

    // Update is called once per frame
    void Update () {
        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ShootingDirection = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                if(Physics.Raycast(ShootingDirection, out hit, PortéeCouteau)){
                    print("Objet Détecté");
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
