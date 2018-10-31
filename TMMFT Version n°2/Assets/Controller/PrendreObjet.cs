using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PrendreObjet : NetworkBehaviour
{

    public int ForceDeJeter;
    public float PortéePourPrendreObjet;

    public Vector3 DirectionForceJet;

    public GameObject PlacePourObjet;

    public GameObject pistolet;
    public GameObject fusil;
    public GameObject mitrailleur1;
    public GameObject mitrailleur2;
    public GameObject bazooka;
    public GameObject Couteau;
    public GameObject Loupe;
    public GameObject TextLoupe;
	public GameObject PistoletLaser;
	public GameObject TextPistoletLaser;

    public bool ObjetPris;

    private string ObjectName;
    private GameObject ObjectQuiEstPris;

    private WeaponChange weaponChange;
	private NetworkSpawner netSpawner;
	
	private GameObject cam;

    // Use this for initialization
    void Start()
    {
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
		
		cam = GameObject.FindWithTag("localCamera");
		
        ObjetPris = false;
        weaponChange = gameObject.GetComponent<WeaponChange>();
		netSpawner = gameObject.GetComponent<NetworkSpawner>();
    }
	
    void Update()
    {
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
        if (Input.GetKeyDown(KeyCode.E) && ObjetPris == false)
        {
            RaycastHit hit;
            Ray ShootingDirection = new Ray(cam.transform.position, cam.transform.forward);

            if (Physics.Raycast(ShootingDirection, out hit, PortéePourPrendreObjet))
            {
                ObjectName = hit.collider.gameObject.name;

                Transform PositionObjet = hit.transform.GetComponent<Transform>();
                Rigidbody rigidbody = hit.transform.GetComponent<Rigidbody>();

                if (hit.collider.tag == "Objet")
                {
                    ObjetPris = true;
					
					if(isServer){
						weaponChange.RpcWSetPistoletPassive();
						weaponChange.RpcWSetFusilPassive();
						weaponChange.RpcWSetMitrailleur1Passive();
						weaponChange.RpcWSetMitrailleur2Passive();
						weaponChange.RpcWSetBazookaPassive();
						weaponChange.RpcWSetCouteauPassive();
						weaponChange.RpcWSetLoupePassive();
						weaponChange.RpcWSetPistoletLaserPassive();
					}else{
						weaponChange.CmdWSetPistoletPassive();
						weaponChange.CmdWSetFusilPassive();
						weaponChange.CmdWSetMitrailleur1Passive();
						weaponChange.CmdWSetMitrailleur2Passive();
						weaponChange.CmdWSetBazookaPassive();
						weaponChange.CmdWSetCouteauPassive();
						weaponChange.CmdWSetLoupePassive();
						weaponChange.CmdWSetPistoletLaserPassive();
					}
					
					int val = int.Parse( gameObject.GetComponent<NetworkIdentity>().netId.ToString() );
					string path = GetGameObjectPath(hit.collider.gameObject);
					if(isServer){
						netSpawner.RpcSyncHandParent(hit.collider.gameObject, val);
					}else{
						netSpawner.CmdSyncHandParent(path, val);
					}
					
                    PositionObjet.transform.rotation = PlacePourObjet.transform.rotation;
                    PositionObjet.transform.position = PlacePourObjet.transform.position;
                    rigidbody.isKinematic = true;
                }

                if (hit.collider.tag == "Loupe")
                {
                    weaponChange.LoupeObtenue = true;
					if(isServer){
						weaponChange.RpcWSetLoupeActive();
					}else{
						weaponChange.CmdWSetLoupeActive();
					}
					
                    Destroy(hit.collider.gameObject);
                }

				if (hit.collider.tag == "Pistolet Laser")
				{
					weaponChange.PistoletLaserObtenue = true;
					
					if(isServer){
						weaponChange.RpcWSetPistoletLaserActive();
					}else{
						weaponChange.CmdWSetPistoletLaserActive();
					}

					Destroy(hit.collider.gameObject);
				}

				if (hit.collider.tag == "Meuble" && hit.collider.isTrigger == true)
				{
					print (hit.collider.material.name);
					if (hit.collider.material.name == "Wood (Instance)") {
						hit.transform.SendMessage ("OuvrirGauche", SendMessageOptions.DontRequireReceiver);
					}

					if (hit.collider.material.name == "Metal (Instance)") {
						hit.transform.SendMessage ("OuvrirDroite", SendMessageOptions.DontRequireReceiver);
					}
				}
				if (hit.collider.tag == "Coffre" && hit.collider.isTrigger == true)
				{
					print (hit.collider.material.name);
					hit.transform.SendMessage ("Ouvrir", SendMessageOptions.DontRequireReceiver);
				}
            }

        }

        if (Input.GetButtonDown("Fire1") && ObjetPris == true)
        {
            ObjectQuiEstPris = GameObject.Find(ObjectName);

            Collider ColliderDeLobjetPris = ObjectQuiEstPris.transform.GetComponent<Collider>();

            Rigidbody rigidbodyDeLObjetPris = ObjectQuiEstPris.transform.GetComponent<Rigidbody>();

			if (rigidbodyDeLObjetPris.useGravity == false) {
				RaycastHit hit;
				Ray ShootingDirection = new Ray(cam.transform.position, cam.transform.forward);

				if(Physics.Raycast(ShootingDirection, out hit)){
					DirectionForceJet = hit.point - ObjectQuiEstPris.transform.position;
					DirectionForceJet = DirectionForceJet / hit.distance;
					//DirectionForceJet = cam.transform.forward;
				}

			} else {
				DirectionForceJet = cam.transform.forward + new Vector3 (0f, 0.4f, 0f);
			}

            Debug.DrawRay(transform.position, DirectionForceJet *4, Color.green);
			
			rigidbodyDeLObjetPris.isKinematic = false;
			ColliderDeLobjetPris.enabled = true;
			
			rigidbodyDeLObjetPris.AddForce(DirectionForceJet * ForceDeJeter);	
			
			if(isServer){
				netSpawner.RpcObjectSend(ObjectQuiEstPris, DirectionForceJet * ForceDeJeter);
			}else{
				netSpawner.CmdObjectSend(GetGameObjectPath(ObjectQuiEstPris), DirectionForceJet * ForceDeJeter);
			}
			
			ObjectQuiEstPris.transform.parent = null;
			
            ObjetPris = false;
        }
    }
	
	public static string GetGameObjectPath(GameObject obj)
	{
		string path = "/" + obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			path = "/" + obj.name + path;
		}
		return path;
	}
}
