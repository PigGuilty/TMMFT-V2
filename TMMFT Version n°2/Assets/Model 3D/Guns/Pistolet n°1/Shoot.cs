using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]

public class Shoot : NetworkBehaviour {

    // Use this for initialization
    public float DegatArme;

    private Camera fpsCam;
    public ParticleSystem Tire;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;
    public GameObject balle;

    public AudioClip piou;
	
	public bool m_isServer;
	
	private NetworkSpawner netSpawner;

    void Start() {
		if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag != "localPlayer")
		{
			return;
		}
		netSpawner = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<NetworkSpawner>();
		fpsCam = GameObject.FindWithTag("localCamera").GetComponent<Camera>();
    }

    void Update () {	
		m_isServer = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<FirstPersonController>().isServer;
		
		if (gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag != "localPlayer")
		{
			return;
		}
		
        Vector3 lookRot = fpsCam.transform.forward;

        if (Input.GetButtonDown("Fire1"))
        {
            Tire.Play();

            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = piou;
            audio.Play();

            RaycastHit hit;
            Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

			if(!m_isServer){
				netSpawner.CmdShoot(fpsCam.transform.position, fpsCam.transform.forward, DegatArme);
			}else{
				netSpawner.RpcShoot(fpsCam.transform.position, fpsCam.transform.forward, DegatArme);
			}
			
            if (Physics.Raycast(ShootingDirection, out hit))
            {

                Target target = hit.transform.GetComponent<Target>();

                if (hit.collider.tag == "Decor interractif")
                {
                    hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                }

                if (hit.collider.tag == "Vache")
                {
                    target.TakeDamage(DegatArme,true);


                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * DegatArme * 4);
                    }
                }

                if (hit.collider.tag != "Player" && hit.collider.tag != "Vache" && hit.collider.tag != "Decor interractif")
                {
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
                    if(m_isServer){
						GameObject balleGO = Instantiate(balle, hit.point, Quaternion.LookRotation(lookRot));
						NetworkServer.Spawn(balleGO);
						balleGO.GetComponent<Apparition>().camToFollow = fpsCam.gameObject;
						
						Destroy(balleGO, 10f);
					}else{
						netSpawner.Spawn(balle, hit.point, Quaternion.LookRotation(lookRot), 10, "apparition:"+fpsCam.transform.parent.GetComponent<NetworkIdentity>().netId.Value);
					}
                }
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
