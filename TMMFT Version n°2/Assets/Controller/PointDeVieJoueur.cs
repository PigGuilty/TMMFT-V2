﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointDeVieJoueur : MonoBehaviour {

    private Camera fpsCam;

    private int PVMax;
    public int PV;
    public int DegatReçus;
    public int PVReçus;
    private bool Mort;

    private float widthOfLiveBar;

    public GameObject SpawnDuJoueur;
    private GameObject NouvelleVersion;

    public GameObject SpawnQuandMort;
    public GameObject Canvas;
    public GameObject BarreDeVie;

    private Attaque attaque;
    private Baguette baguette;
    private WeaponChange weaponchange;
    private Furry furryScript;

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
        gameObject.transform.position = SpawnDuJoueur.transform.position;

        PVMax = 100;
        PV = PVMax;
        Mort = false;

        bloodImage = BloodScreen.GetComponent<Image>();
        BloodScreenOpacity = 0f;
        couleurBloodScreen = new Color(1f, 1f, 1f, 0f);

        attaque = Couteau.GetComponent<Attaque>();
        furryScript = Couteau.GetComponent<Furry>();
        baguette = Couteau.GetComponent<Baguette>();
        weaponchange = gameObject.GetComponent<WeaponChange>();

        fpsCam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        if (Mort == true)
        {
            transform.position = SpawnQuandMort.transform.position;

            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

                if (Physics.Raycast(ShootingDirection, out hit))
                {
                    if (hit.collider.tag == "Decor interractif")
                    {
                        hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }

        else
        {
            if (PV <= 0)
            {
                meurt();
            }

            if (PV > PVMax)
            {
                PV = PVMax;
            }

            var theBarRectTransform = BarreDeVie.transform as RectTransform;
            theBarRectTransform.sizeDelta = new Vector2(PV, theBarRectTransform.sizeDelta.y);

            if (FullOpaque == false)
            {
                float Pv = PV;
                BloodScreenOpacity = ((Pv / 100) * -1) + 1;

                couleurBloodScreen.a = BloodScreenOpacity;
                bloodImage.color = couleurBloodScreen;
            }

            if (FullOpaque == true)
            {
                if (increase == 0)
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

        if (other.gameObject.tag == "KillZone")
        {
            meurt();
        }
    }

    void meurt()
    {
        var theBarRectTransform = furryScript.BarreDeFurry.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(0, theBarRectTransform.sizeDelta.y);
        furryScript.animator.SetFloat("Attaque Couteau", 0f);
        furryScript.AnimationWaitEnd = 0;
        attaque.enabled = true;
        furryScript.AnimBaguetteVersCouteau = false;
        furryScript.AnimCouteauVersBaguette = false;
        attaque.furry = 0;
        weaponchange.BlockLeChangementDArme = true;
        furryScript.AmenoPlayer.enabled = false;
        baguette.enabled = false;

        Canvas.SetActive(false);
        pistolet.SetActive(false);
        fusil.SetActive(false);
        mitrailleur1.SetActive(false);
        mitrailleur2.SetActive(false);
        bazooka.SetActive(false);
        Couteau.SetActive(false);

        Mort = true;
    }

    public void Appuyé()
    {
        Canvas.SetActive(true);

        NouvelleVersion = Instantiate(gameObject);
        Destroy(gameObject);
    }
}
