using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Brule : NetworkBehaviour {

    private Target target;

    public GameObject FeuPlayer;

    GameObject FeuVacheInstanciate;
	
	[SyncVar]
    private bool BruleVache;

    public float TempsPourArreterDeBruler;
    public float TempsEntreChaquePvPerdu;
    private float increase;

	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
	
    // Use this for initialization
    void Start () {
        BruleVache = false;
		netSpawner = GameObject.FindWithTag("localPlayer").GetComponent<NetworkSpawner>();
		
		m_isServer = GameObject.FindWithTag("localPlayer").GetComponent<FirstPersonController>().isServer;
    }
	
	// Update is called once per frame
	void Update () {
        if (BruleVache == true && target.Vie > 0)
        {
            if (increase >= TempsEntreChaquePvPerdu)
            {
                target.TakeDamage(1, false);
                increase = 0.0f;
            }
            else
            {
                increase += Time.deltaTime;
            }
        }
    }

    private void DébutBrule()
    {
        if (target == null)
        {
            target = gameObject.GetComponent<Target>();
        }
        else
        {
            StopCoroutine("BruleStopVache");
            BruleVache = true;

            bool NePasInstantiate = false;

            foreach (Transform child in transform)
            {
                if (child.tag == "Feu")
                {
                    NePasInstantiate = true;
                }
            }
            if (NePasInstantiate == false)
            {
				if(m_isServer){
					FeuVacheInstanciate = Instantiate(FeuPlayer, transform);
					NetworkServer.Spawn(FeuVacheInstanciate);
					FeuVacheInstanciate.transform.position = transform.position;
				}else{
					netSpawner.Spawn(FeuPlayer, transform.position, Quaternion.identity, -1, "");
				}
            }
        }
    }

    private void StopBrule()
    {
        StartCoroutine("BruleStopVache");
    }

    private IEnumerator BruleStopVache()
    {
        yield return new WaitForSeconds(TempsPourArreterDeBruler);
        BruleVache = false;
        if (FeuVacheInstanciate != null)
        {
            foreach (Transform child in transform)
            {
               if (child.tag == "Feu")
               {
                   Destroy(child.gameObject);
               }
           }
        }
    }
}
