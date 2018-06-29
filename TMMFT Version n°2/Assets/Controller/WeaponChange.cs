using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]

public class WeaponChange : NetworkBehaviour {

	public GameObject pistolet;
	public GameObject fusil;
	public GameObject mitrailleur1;
	public GameObject mitrailleur2;
    public GameObject bazooka;
    public GameObject Couteau;

    public GameObject Loupe;
	public GameObject PistoletLaser;

    private int IDWeaponDemandé;
    private bool DemandeChangementArme;
    public bool BlockLeChangementDArme;

    PrendreObjet prendreobjet;

    public AudioClip changement;

    public bool LoupeObtenue;
	public bool PistoletLaserObtenue;
	
    // Use this for initialization
    void Start () {
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
        prendreobjet = gameObject.GetComponent<PrendreObjet>();

        IDWeaponDemandé = 1;
        DemandeChangementArme = true;
        BlockLeChangementDArme = false;
        LoupeObtenue = false;
    }
	
	// Update is called once per frame
	void Update () {		
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
		
        if (prendreobjet.ObjetPris != true && BlockLeChangementDArme != true)
        {
            /****Début Changement D'arme par Clavier****/

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                DemandeChangementArme = true;
            }

            if (Input.GetKeyDown(KeyCode.L) && LoupeObtenue == true)
            {
                DemandeChangementArme = true;
                IDWeaponDemandé = 6;
            }

			if (Input.GetKeyDown(KeyCode.P) && PistoletLaserObtenue == true)
			{
				DemandeChangementArme = true;
				IDWeaponDemandé = 7;
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

					/**scrool up**/
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && IDWeaponDemandé < 5) 
            {
                IDWeaponDemandé++;
                DemandeChangementArme = true;
            }

			if (Input.GetAxis("Mouse ScrollWheel") > 0 && IDWeaponDemandé > 5) 
			{
				IDWeaponDemandé = 5;
				DemandeChangementArme = true;
			}
					/**scrool up**/

					/**scrool down**/

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && IDWeaponDemandé > 1)
            {
                IDWeaponDemandé--;
                DemandeChangementArme = true;
            }

			if (Input.GetAxis("Mouse ScrollWheel") < 0 && IDWeaponDemandé > 5) 
			{
				IDWeaponDemandé = 1;
				DemandeChangementArme = true;
			}
					/**scrool down**/

            /****Fin Changement D'arme par Souris****/


            if (DemandeChangementArme == true)
            {
				if(isServer){
					RpcWSetPistoletPassive();
					RpcWSetFusilPassive();
					RpcWSetMitrailleur1Passive();
					RpcWSetMitrailleur2Passive();
					RpcWSetBazookaPassive();
					RpcWSetCouteauPassive();
					RpcWSetLoupePassive();
					RpcWSetPistoletLaserPassive();
				}else{
					CmdWSetPistoletPassive();
					CmdWSetFusilPassive();
					CmdWSetMitrailleur1Passive();
					CmdWSetMitrailleur2Passive();
					CmdWSetBazookaPassive();
					CmdWSetCouteauPassive();
					CmdWSetLoupePassive();
					CmdWSetPistoletLaserPassive();
				}

                if (IDWeaponDemandé == 1)
                {
					if(isServer){
						RpcWSetPistoletActive();
                    }else{
						CmdWSetPistoletActive();
					}
                }
                else if (IDWeaponDemandé == 2)
                {
					if(isServer){
						RpcWSetFusilActive();
                    }else{
						CmdWSetFusilActive();
					}
                }
                else if (IDWeaponDemandé == 3)
                {
					if(isServer){
						RpcWSetMitrailleur1Active();
						RpcWSetMitrailleur2Active();
                    }else{
						CmdWSetMitrailleur1Active();
						CmdWSetMitrailleur2Active();
					}
                }
                else if (IDWeaponDemandé == 4)
                {
					if(isServer){
						RpcWSetBazookaActive();
                    }else{
						CmdWSetBazookaActive();
					}
                }
                else if (IDWeaponDemandé == 5)
                {
					if(isServer){
						RpcWSetCouteauActive();
                    }else{
						CmdWSetCouteauActive();
					}
                }
                else if (IDWeaponDemandé == 6)
                {
					if(isServer){
						RpcWSetLoupeActive();
                    }else{
						CmdWSetLoupeActive();
					}
                }

				else if (IDWeaponDemandé == 7)
				{
					if(isServer){
						RpcWSetPistoletLaserActive();
                    }else{
						CmdWSetPistoletLaserActive();
					}
				}

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = changement;
                audio.Play();

                DemandeChangementArme = false;
            }
        }
	}
	
	//PISTOLET
	
	[Command]
	public void CmdWSetPistoletActive(){
		pistolet.SetActive(true);
		RpcWSetPistoletActive();
	}
	
	[Command]
	public void CmdWSetPistoletPassive(){
		pistolet.SetActive(false);
		RpcWSetPistoletPassive();
	}
	
	[ClientRpc]
	public void RpcWSetPistoletActive(){
		pistolet.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetPistoletPassive(){
		pistolet.SetActive(false);
	}
	
	// FUSIL
	
	[Command]
	public void CmdWSetFusilActive(){
		fusil.SetActive(true);
		RpcWSetFusilActive();
	}
	
	[Command]
	public void CmdWSetFusilPassive(){
		fusil.SetActive(false);
		RpcWSetFusilPassive();
	}
	
	[ClientRpc]
	public void RpcWSetFusilActive(){
		fusil.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetFusilPassive(){
		fusil.SetActive(false);
	}
	
	// MIT 1
	
	[Command]
	public void CmdWSetMitrailleur1Active(){
		mitrailleur1.SetActive(true);
		RpcWSetMitrailleur1Active();
	}
	
	[Command]
	public void CmdWSetMitrailleur1Passive(){
		mitrailleur1.SetActive(false);
		RpcWSetMitrailleur1Passive();
	}
	
	[ClientRpc]
	public void RpcWSetMitrailleur1Active(){
		mitrailleur1.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetMitrailleur1Passive(){
		mitrailleur1.SetActive(false);
	}
	
	// MIT 2
	
	[Command]
	public void CmdWSetMitrailleur2Active(){
		mitrailleur2.SetActive(true);
		RpcWSetMitrailleur2Active();
	}
	
	[Command]
	public void CmdWSetMitrailleur2Passive(){
		mitrailleur2.SetActive(false);
		RpcWSetMitrailleur2Passive();
	}
	
	[ClientRpc]
	public void RpcWSetMitrailleur2Active(){
		mitrailleur2.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetMitrailleur2Passive(){
		mitrailleur2.SetActive(false);
	}
	
	// BAZOOOKA
	
	[Command]
	public void CmdWSetBazookaActive(){
		bazooka.SetActive(true);
		RpcWSetBazookaActive();
	}
	
	[Command]
	public void CmdWSetBazookaPassive(){
		bazooka.SetActive(false);
		RpcWSetBazookaPassive();
	}
	
	[ClientRpc]
	public void RpcWSetBazookaActive(){
		bazooka.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetBazookaPassive(){
		bazooka.SetActive(false);
	}
	
	// COUTEAU
	
	[Command]
	public void CmdWSetCouteauActive(){
		Couteau.SetActive(true);
		RpcWSetCouteauActive();
	}
	
	[Command]
	public void CmdWSetCouteauPassive(){
		Couteau.SetActive(false);
		RpcWSetCouteauPassive();
	}
	
	[ClientRpc]
	public void RpcWSetCouteauActive(){
		Couteau.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetCouteauPassive(){
		Couteau.SetActive(false);
	}
	
	// LOUPE
	
	[Command]
	public void CmdWSetLoupeActive(){
		Loupe.SetActive(true);
		RpcWSetLoupeActive();
	}
	
	[Command]
	public void CmdWSetLoupePassive(){
		Loupe.SetActive(false);
		RpcWSetLoupePassive();
	}
	
	[ClientRpc]
	public void RpcWSetLoupeActive(){
		Loupe.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetLoupePassive(){
		Loupe.SetActive(false);
	}
	
	// PISTOLET LASER
	
	[Command]
	public void CmdWSetPistoletLaserActive(){
		PistoletLaser.SetActive(true);
		RpcWSetPistoletLaserActive();
	}
	
	[Command]
	public void CmdWSetPistoletLaserPassive(){
		PistoletLaser.SetActive(false);
		RpcWSetPistoletLaserPassive();
	}
	
	[ClientRpc]
	public void RpcWSetPistoletLaserActive(){
		PistoletLaser.SetActive(true);
	}
	
	[ClientRpc]
	public void RpcWSetPistoletLaserPassive(){
		PistoletLaser.SetActive(false);
	}

}
