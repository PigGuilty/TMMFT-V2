using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDeVieJoueur : MonoBehaviour {

    private int PVMax;
    public int PV;
    public int DegatReçus;
    public int PVReçus;

    private float widthOfLiveBar;

    public GameObject SpawnQuandMort;
    public GameObject Canvas;
    public GameObject BarreDeVie;

    public GameObject pistolet;
    public GameObject fusil;
    public GameObject mitrailleur1;
    public GameObject mitrailleur2;
    public GameObject bazooka;
    public GameObject Couteau;

    public GameObject BloodScreen;
    private Image bloodImage;
    private Color couleurBloodScreen;

    private float BloodScreenOpacity;
    private bool FullOpaque;
    private int increase;

    // Use this for initialization
    void Start () {
        PVMax = 100;
        PV = PVMax;

        bloodImage = BloodScreen.GetComponent<Image>();
        BloodScreenOpacity = 0f;
        couleurBloodScreen = new Color(1f, 1f, 1f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if ( PV <= 0)
        {
            meurt();
        }

        if (PV > PVMax)
        {
            PV = PVMax;
        }

        var theBarRectTransform = BarreDeVie.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(PV, theBarRectTransform.sizeDelta.y);

        if(FullOpaque == false)
        {
            float Pv = PV;
            BloodScreenOpacity = ((Pv / 100) * -1) + 1;

            couleurBloodScreen.a = BloodScreenOpacity;
            bloodImage.color = couleurBloodScreen;
        }

        if(FullOpaque == true)
        {
            if(increase == 0)
            {
                bloodImage.color = Color.white;
            }

            increase++;

            if (increase >= 4)
            {
                increase = 0;
                FullOpaque = false;
            }
        }
}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vache")
        {
            PV = PV - DegatReçus;
            FullOpaque = true;
        }

        if (other.gameObject.tag == "Heal")
        {
            PV = PV + PVReçus;
        }
    }

    void meurt()
    {
        transform.position = SpawnQuandMort.transform.position;

        Canvas.SetActive(false);
        pistolet.SetActive(false);
        fusil.SetActive(false);
        mitrailleur1.SetActive(false);
        mitrailleur2.SetActive(false);
        bazooka.SetActive(false);
        Couteau.SetActive(false);
    }
}
