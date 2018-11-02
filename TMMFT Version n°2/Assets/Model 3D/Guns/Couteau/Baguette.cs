using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Baguette : NetworkBehaviour {

    public float DegatArme;
    private float NouveauDegat;
    private float FacteurMultiplicateur;

    private GameObject fpsCam;

    public GameObject LASER;
	public GameObject player;

    private Attaque attaque;
    private float ok;
    private float lengthOfRaycast;

    RaycastHit hit;

    public GameObject GameObjectparticleSystemLaser1;
    public GameObject GameObjectparticleSystemLaser2;
	//public GameObject GameObjectparticleSystemLaser3;

    private ParticleSystem particleSystemLaser1;
    private ParticleSystem particleSystemLaser2;
	//private ParticleSystem particleSystemLaser3;

    private AudioSource Audio;
    public AudioSource AudioAmeno;
    public AudioClip SonMagique;

    private bool alreadyActiveted;
	private GameObject Player;

    private void Start() {
		Player = gameObject.transform.parent.parent.parent.gameObject;
		
		if (!(Player.tag == "localPlayer"))
		{
			return;
		}
		attaque = GetComponent<Attaque> ();
        particleSystemLaser1 = GameObjectparticleSystemLaser1.GetComponent<ParticleSystem>();
		print(particleSystemLaser1);	
        particleSystemLaser2 = GameObjectparticleSystemLaser2.GetComponent<ParticleSystem>();
		//particleSystemLaser3 = GameObjectparticleSystemLaser3.GetComponent<ParticleSystem>();

        particleSystemLaser1.enableEmission = false;
        particleSystemLaser2.enableEmission = false;
		//particleSystemLaser3.enableEmission = false;
        ok = 0f;

        fpsCam = GameObject.FindWithTag("localCamera");

        Audio = gameObject.GetComponent<AudioSource>();

        alreadyActiveted = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!(Player.tag == "localPlayer"))
		{
			return;
		}
        if (ok >= 0.061f && Input.GetButton ("Fire1"))
        {
            attaque.furry = attaque.furry - 1;
            ok = 0;
        }
		if (ok < 0.061f && Input.GetButton ("Fire1"))
        {
			ok += Time.deltaTime;
        }

		if(Input.GetButtonDown("Fire1"))
			player.transform.SendMessage("Shake", SendMessageOptions.DontRequireReceiver);
        
	
        if (Input.GetButton("Fire1"))
        {
            if (alreadyActiveted == false)
            {
                Audio.clip = SonMagique;
                Audio.Play();
                AudioAmeno.volume = 0;
                alreadyActiveted = true;
            }

            particleSystemLaser1.enableEmission = true;
            particleSystemLaser2.enableEmission = true;
			//particleSystemLaser3.enableEmission = true;

            Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

            lengthOfRaycast = hit.distance;
            particleSystemLaser1.startLifetime = lengthOfRaycast / 15;
            particleSystemLaser2.startLifetime = lengthOfRaycast / 15;


            if (Physics.Raycast(ShootingDirection, out hit))
            {

                Target target = hit.transform.GetComponent<Target>();

                if (hit.collider.tag == "Decor interractif")
                {
                    hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                }

                if (hit.collider.tag == "Vache")
                {

                    FacteurMultiplicateur = (1.0f / Time.deltaTime) / 30;
                    NouveauDegat = DegatArme / FacteurMultiplicateur;
                    target.TakeDamage(NouveauDegat,true);

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * DegatArme * 50);
                    }
                }

            }
        }

        if (Input.GetButtonUp ("Fire1")) {
			print("stop");
			player.transform.SendMessage ("Stop", SendMessageOptions.DontRequireReceiver);
            particleSystemLaser1.enableEmission = false;
            particleSystemLaser2.enableEmission = false;
			//particleSystemLaser3.enableEmission = false;
            Audio.Stop();
            AudioAmeno.volume = 0.75f;
            alreadyActiveted = false;
        }
    }

	void OnDisable(){
		player.transform.SendMessage ("Stop", SendMessageOptions.DontRequireReceiver);
        particleSystemLaser1.enableEmission = false;
        particleSystemLaser2.enableEmission = false;
		//particleSystemLaser3.enableEmission = false;
        Audio.Stop();
        AudioAmeno.volume = 0.75f;
        alreadyActiveted = false;
    }

    private void OnEnable()
    {
        LASER.SetActive(true);
    }
}
