﻿using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]

public class ShootFAP : NetworkBehaviour
{

    public float DegatArme;
	public float nbProjctiles;
	public float Dispertion;
    public int TailleChargeur;
    private int BalleRestante;

    public ParticleSystem Tire;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;
    public GameObject balle;
    public GameObject SpawnBullet;

    private Animator animator;

    private float AnimationLength;
    private float AnimationWaitEnd;

    public AudioSource audioFAP;

    public AudioClip Paf;
    public AudioClip Rechargement;

    private bool douilleApparue;
	
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;
    private Camera fpsCam;
	
    void Start()
    {
		if (gameObject.transform.parent.parent.parent.tag != "localPlayer")
		{
			return;
		}
		netSpawner = gameObject.transform.parent.parent.parent.GetComponent<NetworkSpawner>();
		fpsCam = GameObject.FindWithTag("localCamera").GetComponent<Camera>();
		
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 0.83f;
        AnimationWaitEnd = 0;
        douilleApparue = false;
    }
    
    // Update is called once per frame
    void Update()
    {
		if (gameObject.transform.parent.parent.parent.tag != "localPlayer")
		{
			return;
		}
		m_isServer = gameObject.transform.parent.parent.parent.GetComponent<FirstPersonController>().isServer;
		
        Vector3 lookRot = fpsCam.transform.forward;

        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {

                Tire.Play();

                audioFAP = gameObject.GetComponent<AudioSource>();
                audioFAP.clip = Paf;
                audioFAP.Play();

                RaycastHit hit;
				for (int i = 0; i < nbProjctiles; i++) {
					Vector3 dir = fpsCam.transform.forward;
					dir.x = dir.x * (Dispertion / 100) + Random.Range (-0.01f, 0.01f);
					dir.y = dir.y * (Dispertion / 100) + Random.Range (-0.01f, 0.01f);
					dir.z = dir.z * (Dispertion / 100) + Random.Range (-0.01f, 0.01f);

					Ray ShootingDirection = new Ray (fpsCam.transform.position, dir);
					
					if(!m_isServer){
						netSpawner.CmdShoot(fpsCam.transform.position, dir, DegatArme);
					}else{
						netSpawner.RpcShoot(fpsCam.transform.position, dir, DegatArme);
					}

					if (Physics.Raycast (ShootingDirection, out hit)) {

						Target target = hit.transform.GetComponent<Target> ();

                        if (hit.collider.tag == "Decor interractif")
                        {
                            hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                        }

                        if (hit.collider.tag == "Vache") {
							target.TakeDamage (DegatArme, true);

							if (hit.rigidbody != null) {
								hit.rigidbody.AddForce (-hit.normal * DegatArme * 4);
							}
						}

						if (hit.collider.tag != "Player" && hit.collider.tag != "Vache" && hit.collider.tag != "Decor interractif") {
                            float x = Random.Range(0.0f, 4.0f);
						if (x <= 1)
						{
							if(m_isServer){
								GameObject impactGO1 = Instantiate(impactEffect1, hit.point, Quaternion.LookRotation(hit.normal));
								impactGO1.transform.Translate(hit.normal / 1000, Space.World);
								impactGO1.transform.parent = hit.transform;
								
								NetworkServer.Spawn(impactGO1);
								netSpawner.RpcSyncImpact(impactGO1, GetGameObjectPath(hit.transform.gameObject));
								
								Destroy(impactGO1, 20f);
							}else{
								netSpawner.Spawn(impactEffect1, hit.point, Quaternion.LookRotation(hit.normal), 10, "impact:"+hit.normal+":"+GetGameObjectPath(hit.transform.gameObject));
							}
						}
						else if (x > 1 && x <= 2)
						{
							if(m_isServer){
								GameObject impactGO2 = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
								impactGO2.transform.Translate(hit.normal / 1000, Space.World);
								impactGO2.transform.parent = hit.transform;
								
								NetworkServer.Spawn(impactGO2);
								netSpawner.RpcSyncImpact(impactGO2, GetGameObjectPath(hit.transform.gameObject));
								
								Destroy(impactGO2, 20f);
							}else{
								netSpawner.Spawn(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal), 10, "impact:"+hit.normal+":"+GetGameObjectPath(hit.transform.gameObject));
							}
						}
						else if (x > 2 && x <= 3)
						{
							if(m_isServer){
								GameObject impactGO3 = Instantiate(impactEffect3, hit.point, Quaternion.LookRotation(hit.normal));
								impactGO3.transform.Translate(hit.normal / 1000, Space.World);
								impactGO3.transform.parent = hit.transform;
								
								NetworkServer.Spawn(impactGO3);
								netSpawner.RpcSyncImpact(impactGO3, GetGameObjectPath(hit.transform.gameObject));
								
								Destroy(impactGO3, 20f);
							}else{
								netSpawner.Spawn(impactEffect3, hit.point, Quaternion.LookRotation(hit.normal), 10, "impact:"+hit.normal+":"+GetGameObjectPath(hit.transform.gameObject));
							}
						}
						else if (x > 3 && x <= 4)
						{                        
							if(m_isServer){
								GameObject impactGO4 = Instantiate(impactEffect4, hit.point, Quaternion.LookRotation(hit.normal));
								impactGO4.transform.Translate(hit.normal / 1000, Space.World);
								impactGO4.transform.parent = hit.transform;
								
								NetworkServer.Spawn(impactGO4);
								netSpawner.RpcSyncImpact(impactGO4, GetGameObjectPath(hit.transform.gameObject));
								
								Destroy(impactGO4, 20f);
							}else{
								netSpawner.Spawn(impactEffect4, hit.point, Quaternion.LookRotation(hit.normal), 10, "impact:"+hit.normal+":"+GetGameObjectPath(hit.transform.gameObject));
							}
						}
                        }
					}
				}
                BalleRestante = BalleRestante - 1;

            }
        }

        if (BalleRestante <= 0)
        {
            
            if (AnimationWaitEnd == 0)
            {
                animator.Rebind();
                animator.SetFloat("Reload", 1);
            }

            AnimationWaitEnd += Time.deltaTime;

            if (AnimationWaitEnd >= AnimationLength/4 && douilleApparue == false)
            {
                Vector3 lookrot2 = new Vector3(lookRot.x + Random.Range(-0.4f, 0.4f), lookRot.y + Random.Range(-0.4f, 0.4f), lookRot.z + Random.Range(-0.4f, 0.4f));
				
				if(m_isServer){
					GameObject balleGO = Instantiate(balle, SpawnBullet.transform.position, Quaternion.LookRotation(lookrot2));
					NetworkServer.Spawn(balleGO);
					balleGO.GetComponent<Apparitiondouille>().camToFollow = fpsCam.gameObject;
					
					Destroy(balleGO, 10f);
				}else{
					netSpawner.Spawn(balle, SpawnBullet.transform.position, Quaternion.LookRotation(lookrot2), 10, "apparitionFAP:"+fpsCam.transform.parent.GetComponent<NetworkIdentity>().netId.Value);
				}

                audioFAP = gameObject.GetComponent<AudioSource>();
                audioFAP.clip = Rechargement;
                audioFAP.Play();

                douilleApparue = true;
            }
                //attendre fin de l'annimation
                if (AnimationWaitEnd >= AnimationLength)
            {
                animator.SetFloat("Reload", 0);
                BalleRestante = TailleChargeur;
                AnimationWaitEnd = 0;
                douilleApparue = false;
            }
            
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
