using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalierScript : MonoBehaviour
{

    private Animator animator;

    public GameObject MarcheSuivante;

    private bool appuyé;
    private bool relaché;
    private bool ChangementEtat;

    private float increase;

    void Start()
    {
        animator = GetComponent<Animator>();

        appuyé = false;
        relaché = false;

        increase = 0;
    }

    void Update()
    {
        if(ChangementEtat == true)
        {
            if(appuyé == true)
            {
                if(increase == 0)
                {
                    animator.Rebind();
                    animator.SetFloat("Escalier", 1);
                }

                increase += Time.deltaTime;

                if (increase >= 0.05f)
                {
                    MarcheSuivante.transform.SendMessage("Appuyé", SendMessageOptions.DontRequireReceiver);
                    increase = 0;
                    ChangementEtat = false;
                }
            }
            else
            {
                if (increase == 0)
                {
                    animator.Rebind();
                    animator.SetFloat("Escalier", 0);
                }

                increase += Time.deltaTime;

                if (increase >= 0.05f)
                {
                    MarcheSuivante.transform.SendMessage("Relaché", SendMessageOptions.DontRequireReceiver);
                    increase = 0;
                    ChangementEtat = false;
                }
            }
        }
    }

    public void Appuyé()
    {
        ChangementEtat = true;
        appuyé = !appuyé;
    }

    public void Relaché()
    {
        ChangementEtat = true;
        appuyé = !appuyé;
    }
}