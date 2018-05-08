using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PutInFire : MonoBehaviour
{

    private PointDeVieJoueur pvJoueur;

    public float TempsPourArreterDeBruler;
    public float TempsEntreChaquePvPerdu;
    private float increase;

    private bool BruleJoueur;

    public GameObject FeuPlayer;
    private bool FeuPlayerDejaCree;

    GameObject FeuPlayerInstanciate;

    public GameObject FlameLogo;

    // Use this for initialization
    void Start()
    {
        BruleJoueur = false;
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
                FlameLogo.SetActive(true);

                if (FeuPlayerDejaCree == false)
                {
                    FeuPlayerInstanciate = Instantiate(FeuPlayer, other.transform);
                    FeuPlayerInstanciate.transform.position = other.transform.position;
                    FeuPlayerDejaCree = true;
                }
            }
        }

        if (other.gameObject.tag == "Vache")
        {
            other.gameObject.SendMessage("DébutBrule", SendMessageOptions.DontRequireReceiver);
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
            other.gameObject.SendMessage("StopBrule", SendMessageOptions.DontRequireReceiver);
        }
    }

    private IEnumerator BruleStopJoueur()
    {
        yield return new WaitForSeconds(TempsPourArreterDeBruler);
        BruleJoueur = false;
        FeuPlayerDejaCree = false;
        FlameLogo.SetActive(false);
        Destroy(FeuPlayerInstanciate);
    }
}

