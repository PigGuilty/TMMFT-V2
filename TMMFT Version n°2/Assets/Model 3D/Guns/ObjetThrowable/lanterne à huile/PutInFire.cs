using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutInFire : MonoBehaviour
{

    private PointDeVieJoueur pvJoueur;

    public float TempsPourArreterDeBruler;
    public float TempsEntreChaquePvPerdu;
    private float increase;

    private bool BruleJoueur;

    public GameObject FeuPlayer;
    private bool FeuPlayerDejaCree;

    public bool Desactivé;
    public bool GoDestruction;
    private bool Permission;

    GameObject FeuPlayerInstanciate;

    private GameObject FlameLogo;
	private GameObject Canvas;
	
    // Use this for initialization
    void Start()
    {
        Desactivé = false;
        BruleJoueur = false;
        Permission = true;
        Canvas = GameObject.Find("Canvas");
		if(Canvas != null) {
			foreach(Transform child in Canvas.transform)
			{
				if(child.name == "flame")
				{
					FlameLogo = child.gameObject;
				}
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(Canvas == null){
			Canvas = GameObject.Find("Canvas");
			if(Canvas != null) {
				foreach(Transform child in Canvas.transform)
				{
					if(child.name == "flame")
					{
						FlameLogo = child.gameObject;
					}
				}
			}
		}
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

        if(Desactivé == true && Permission == true)
        {
            GoDestruction = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Desactivé == false)
        {
            if (other.gameObject.tag == "Player")
            {
                if (pvJoueur == null)
                {
                    pvJoueur = other.gameObject.GetComponent<PointDeVieJoueur>();
                }
                else
                {
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (Desactivé == false)
        {
            Permission = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(Desactivé == true)
        {
            GoDestruction = true;
        }
        if (GoDestruction == false)
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

