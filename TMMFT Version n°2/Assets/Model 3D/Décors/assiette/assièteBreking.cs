using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class assièteBreking : MonoBehaviour {

    public GameObject Assiète;
    public GameObject AssièteCassé;

    public Collider col;

    private GameObject Player;
    private PrendreObjet prendreobjet;
    private ArmoireScript armoirescript;

    private bool OnALaDir;

    public static Vector3 dir;

    public GameObject JoueurDeSon;

	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
	private FirstPersonController controller;
	
	private GameObject cam;
	
    // Use this for initialization
    void Start () {
        OnALaDir = false;
        col = Assiète.GetComponent<Collider>();
		
		Player = GameObject.FindWithTag("localPlayer");
		if(Player != null){
			prendreobjet = Player.GetComponent<PrendreObjet>();
			controller = Player.GetComponent<FirstPersonController>();
			netSpawner = Player.GetComponent<NetworkSpawner>();
			
			cam = GameObject.FindWithTag("localCamera");
		}
    }

    private void Update()
    {
		if(Player == null){
			Player = GameObject.FindWithTag("localPlayer");
			if(Player != null){
				prendreobjet = Player.GetComponent<PrendreObjet>();
				controller = Player.GetComponent<FirstPersonController>();
				netSpawner = Player.GetComponent<NetworkSpawner>();
				
				cam = GameObject.FindWithTag("localCamera");
			}
		}else{
			if(OnALaDir == false)
			{
				if(col.enabled == true)
				{
					dir = new Vector3(cam.transform.forward.x, 0.05f, cam.transform.forward.z);
					OnALaDir = true;
				}
			}
		}
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (prendreobjet.ObjetPris == false)
        {
            if (other.gameObject.GetComponent<ArmoireScript>() != null)
            {
                armoirescript = other.gameObject.GetComponent<ArmoireScript>();

                if (armoirescript.Ouvert == false)
                {
                    SeCasse();
                }
            }
            else
            {
                SeCasse();
            }
        }
    }

    private void SeCasse()
    {
		if(m_isServer){
			GameObject Assièteinvoqué = Instantiate(AssièteCassé);
			Assièteinvoqué.transform.position = Assiète.transform.position;
			NetworkServer.Spawn(Assièteinvoqué);
		}else{
			netSpawner.Spawn(AssièteCassé, Assiète.transform.position, Quaternion.identity, -1, "");
		}

        GameObject JoueurDeSonInvok = Instantiate(JoueurDeSon);
        JoueurDeSonInvok.transform.position = Assiète.transform.position;

        Destroy(Assiète);
    }
}
