using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInFire : MonoBehaviour
{

    private PointDeVieJoueur pvJoueur;
    private Target target;

    public float TempsPourArreterDeBruler;
    public float TempsEntreChaquePvPerdu;
    private float increase;

    private bool BruleJoueur;
    private bool BruleVache;

    // Use this for initialization
    void Start()
    {
        BruleJoueur = false;
        BruleVache = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(BruleJoueur == true)
        {
            if (increase >= TempsEntreChaquePvPerdu)
            {
                pvJoueur.PV -= 1;
                increase = 0.0f;
            }
            else {
                increase += Time.deltaTime;
            }
        }

        if (BruleVache == true && target.Vie > 0)
        {
            if (increase >= TempsEntreChaquePvPerdu)
            {
                target.TakeDamage(1,false);
                increase = 0.0f;
            }
            else
            {
                increase += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pvJoueur == null)
            {
                pvJoueur = other.gameObject.GetComponent<PointDeVieJoueur>();
            }
            else {
                StopCoroutine("BruleStopJoueur");
                print("StartBurning");
                BruleJoueur = true;
            }
        }

        if (other.gameObject.tag == "Vache")
        {
            if (target == null)
            {
                target = other.gameObject.GetComponent<Target>();
            }
            else
            {
                StopCoroutine("BruleStopVache");
                BruleVache = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("BruleStopJoueur");
        }
        if (other.gameObject.tag == "Vache")
        {
            StartCoroutine("BruleStopVache");
        }
    }

    private IEnumerator BruleStopJoueur()
    {
        yield return new WaitForSeconds(TempsPourArreterDeBruler);
        BruleJoueur = false;
    }

    private IEnumerator BruleStopVache()
    {
        yield return new WaitForSeconds(TempsPourArreterDeBruler);
        BruleVache = false;
    }
}
