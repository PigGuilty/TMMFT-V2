using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PutInFire : MonoBehaviour
{

    private PointDeVieJoueur pvJoueur;
    private Target target;

    public float TempsPourArreterDeBruler;
    public float TempsEntreChaquePvPerdu;
    private float increase;

    private bool BruleJoueur;
    private bool BruleVache;

    public GameObject FeuPlayer;
    private bool FeuPlayerDejaCree;

    GameObject FeuPlayerInstanciate;
    GameObject FeuVacheInstanciate;

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
                BruleJoueur = true;

                if(FeuPlayerDejaCree == false)
                {
                    FeuPlayerInstanciate = Instantiate(FeuPlayer, other.transform);
                    FeuPlayerInstanciate.transform.position = other.transform.position;
                    FeuPlayerDejaCree = true;
                }
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
                if (other.GetComponentsInChildren<AudioSource>() != null)
                {
                    FeuVacheInstanciate = Instantiate(FeuPlayer, other.transform);
                    FeuVacheInstanciate.transform.position = other.transform.position;
                }
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
        FeuPlayerDejaCree = false;
        Destroy(FeuPlayerInstanciate);
    }

    private IEnumerator BruleStopVache()
    {
        yield return new WaitForSeconds(TempsPourArreterDeBruler);
        BruleVache = false;
        Destroy(FeuVacheInstanciate);
    }
}
