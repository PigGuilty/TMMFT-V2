using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brule : MonoBehaviour {

    private Target target;

    public GameObject FeuPlayer;

    GameObject FeuVacheInstanciate;

    private bool BruleVache;

    public float TempsPourArreterDeBruler;
    public float TempsEntreChaquePvPerdu;
    private float increase;

    // Use this for initialization
    void Start () {
        BruleVache = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (BruleVache == true && target.Vie > 0)
        {
            if (increase >= TempsEntreChaquePvPerdu)
            {
                target.TakeDamage(1, false);
                increase = 0.0f;
            }
            else
            {
                increase += Time.deltaTime;
            }
        }
    }

    private void DébutBrule()
    {
        if (target == null)
        {
            target = gameObject.GetComponent<Target>();
        }
        else
        {
            StopCoroutine("BruleStopVache");
            BruleVache = true;

            bool NePasInstantiate = false;

            foreach (Transform child in transform)
            {
                if (child.tag == "Feu")
                {
                    NePasInstantiate = true;
                }
            }
            if (NePasInstantiate == false)
            {
                FeuVacheInstanciate = Instantiate(FeuPlayer, transform);
                FeuVacheInstanciate.transform.position = transform.position;
            }
        }
    }

    private void StopBrule()
    {
        StartCoroutine("BruleStopVache");
    }

    private IEnumerator BruleStopVache()
    {
        yield return new WaitForSeconds(TempsPourArreterDeBruler);
        BruleVache = false;
        if (FeuVacheInstanciate != null)
        {
            foreach (Transform child in transform)
            {
               if (child.tag == "Feu")
               {
                   Destroy(child.gameObject);
               }
           }
        }
    }
}
