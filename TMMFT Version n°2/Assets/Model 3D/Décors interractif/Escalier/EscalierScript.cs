using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalierScript : MonoBehaviour {

    private Animator animator;

    public GameObject MarcheSuivante;

    private bool appuyé;
    private bool relaché;

    private int increase;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        appuyé = false;
        relaché = false;

        increase = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (appuyé == true)
        {
            if(increase >= 2)
            {
                MarcheSuivante.transform.SendMessage("Appuyé", SendMessageOptions.DontRequireReceiver);
                increase = 0;
                appuyé = false;
            }
            increase++;
        }

        if (relaché == true)
        {
            if (increase >= 2)
            {
                MarcheSuivante.transform.SendMessage("Relaché", SendMessageOptions.DontRequireReceiver);
                increase = 0;
                relaché = false;
            }
            increase++;
        }
    }

    public void Appuyé()
    {
        animator.Rebind();
        animator.SetFloat("Escalier", 1);

        appuyé = true;
    }

    public void Relaché()
    {
        animator.Rebind();
        animator.SetFloat("Escalier", 0);

        relaché = true;
    }
}
