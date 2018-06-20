using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {
        OnALaDir = false;
        col = Assiète.GetComponent<Collider>();
		
		Player = GameObject.FindWithTag("Player");
		if(Player != null)
			prendreobjet = Player.GetComponent<PrendreObjet>();
    }

    private void Update()
    {
		if(Player == null){
			Player = GameObject.FindWithTag("Player");
			if(Player != null)
				prendreobjet = Player.GetComponent<PrendreObjet>();
		}else{
			if(OnALaDir == false)
			{
				if(col.enabled == true)
				{
					dir = new Vector3(Camera.main.transform.forward.x, 0.05f, Camera.main.transform.forward.z);
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
        GameObject Assièteinvoqué = Instantiate(AssièteCassé);
        Assièteinvoqué.transform.position = Assiète.transform.position;

        GameObject JoueurDeSonInvok = Instantiate(JoueurDeSon);
        JoueurDeSonInvok.transform.position = Assiète.transform.position;

        Destroy(Assiète);
    }
}
