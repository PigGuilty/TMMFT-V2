using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appuis : MonoBehaviour {

    private Animator animator;

    private bool appuyé;
    private bool EnAnimation;

    private float AnimationWaitEnd;
    private int AnimationLength;

    public GameObject ObjetAvecLequelIlYAInterraction;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        appuyé = false;
        EnAnimation = false;

        AnimationWaitEnd = 0;

        animator.SetFloat("Action", 1);
    }

    public void HitByRaycast()
    {
        if (EnAnimation == false)
        {
            EnAnimation = true;

            if (appuyé == false)
            {
                animator.Rebind();
                animator.SetFloat("Action", 0);
                ObjetAvecLequelIlYAInterraction.transform.SendMessage("Appuyé", SendMessageOptions.DontRequireReceiver);
            }

            if (appuyé == true)
            {
                animator.Rebind();
                animator.SetFloat("Action", 1);
                ObjetAvecLequelIlYAInterraction.transform.SendMessage("Relaché", SendMessageOptions.DontRequireReceiver);
            }

            appuyé = !appuyé;
        }
    }
    // Update is called once per frame
    void Update () {
        if (EnAnimation == true)
        {
            AnimationWaitEnd += Time.deltaTime;
            
            if (AnimationWaitEnd >= 0.65f)
            {
                AnimationWaitEnd = 0;
                EnAnimation = false;
            }
        }
    }
}
