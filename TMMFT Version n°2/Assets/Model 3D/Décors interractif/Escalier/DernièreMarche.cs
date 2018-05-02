using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DernièreMarche : MonoBehaviour
{

    private Animator animator;

    public GameObject Cube;
    public GameObject CubeTigger;

    private BoxCollider CubeBox;
    private BoxCollider CubeTriggerBox;

    public PhysicMaterial mat;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        CubeBox = Cube.GetComponent<BoxCollider>();
        CubeTriggerBox = CubeTigger.GetComponent<BoxCollider>();

        CubeTriggerBox.enabled = false;
        CubeBox.material = null;
    }

    public void Appuyé()
    {
        animator.Rebind();
        animator.SetFloat("Escalier", 1);

        CubeTriggerBox.enabled = true;
        CubeBox.material = mat;
    }

    public void Relaché()
    {
        animator.Rebind();
        animator.SetFloat("Escalier", 0);

        CubeTriggerBox.enabled = false;
        CubeBox.material = null;
    }
}
