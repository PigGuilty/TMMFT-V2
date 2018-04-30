using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WeaponChange : MonoBehaviour {

	public GameObject pistolet;
	public GameObject fusil;
	public GameObject mitrailleur1;
	public GameObject mitrailleur2;
    public GameObject bazooka;
    public GameObject Couteau;

    private int IDWeaponDemandé;
    private bool DemandeChangementArme;

    PrendreObjet prendreobjet;

    public AudioClip changement;

    // Use this for initialization
    void Start () {
        prendreobjet = gameObject.GetComponent<PrendreObjet>();

        IDWeaponDemandé = 1;
        DemandeChangementArme = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (prendreobjet.ObjetPris != true)
        {
            /****Début Changement D'arme par Clavier****/

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                DemandeChangementArme = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IDWeaponDemandé = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                IDWeaponDemandé = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                IDWeaponDemandé = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                IDWeaponDemandé = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                IDWeaponDemandé = 5;
            }

            /****Fin Changement D'arme par Clavier****/

            /****Début Changement D'arme par Souris****/

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && IDWeaponDemandé < 5)
            {
                IDWeaponDemandé++;
                DemandeChangementArme = true;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && IDWeaponDemandé > 1)
            {
                IDWeaponDemandé--;
                DemandeChangementArme = true;
            }

            /****Fin Changement D'arme par Souris****/


            if (DemandeChangementArme == true)
            {
                pistolet.SetActive(false);
                fusil.SetActive(false);
                mitrailleur1.SetActive(false);
                mitrailleur2.SetActive(false);
                bazooka.SetActive(false);
                Couteau.SetActive(false);

                if (IDWeaponDemandé == 1)
                {
                    pistolet.SetActive(true);
                }
                else if (IDWeaponDemandé == 2)
                {
                    fusil.SetActive(true);
                }
                else if (IDWeaponDemandé == 3)
                {
                    mitrailleur1.SetActive(true);
                    mitrailleur2.SetActive(true);
                }
                else if (IDWeaponDemandé == 4)
                {
                    bazooka.SetActive(true);
                }
                else if (IDWeaponDemandé == 5)
                {
                    Couteau.SetActive(true);
                }

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = changement;
                audio.Play();

                DemandeChangementArme = false;
            }
        }
	}
}
