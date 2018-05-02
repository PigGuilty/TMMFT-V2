using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DernièreMarche : MonoBehaviour
{

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Appuyé()
    {
        animator.Rebind();
        animator.SetFloat("Escalier", 1);
    }

    public void Relaché()
    {
        animator.Rebind();
        animator.SetFloat("Escalier", 0);
    }
}
