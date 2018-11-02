using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointDeVieJoueur : NetworkBehaviour {

    private Camera fpsCam;

    private int PVMax;
	[SyncVar]
    public int PV;
    public int DegatReçus;
    public int PVReçus;
    public bool Mort;

    private float widthOfLiveBar;

    public GameObject Canvas;
    public GameObject BarreDeVie;
	public Text Dead;

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
    public GameObject Loupe;
	public GameObject PistoletLaser;

    public GameObject BloodScreen;
    private Image bloodImage;
    private Color couleurBloodScreen;

    private float BloodScreenOpacity;
    private bool FullOpaque;
    private float increase;
	
	private float tpsRespawn = 30;
	private float count = 30;
	
    // Use this for initialization
    void Start () {		
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
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

        fpsCam = GameObject.FindWithTag("localCamera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {		
		if (gameObject.tag != "localPlayer")
		{
			return;
		}
        if (Mort == true)
        {
            transform.position = GameObject.FindWithTag("SpawnDead").transform.position;

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

            else if (FullOpaque == true)
            {
                if (increase == 0.0f)
                {
                    bloodImage.color = Color.white;
                }

                increase += Time.deltaTime;

                if (increase >= 0.2f)
                {
                    increase = 0.0f;
                    FullOpaque = false;
                }
            }
        }
		
		if(Mort && count >= 0 && !isGameOver())
			Dead.text = "Vous êtes mort ! Attendez "+count+" secondes avant de réapparaitre";
		else if(Mort && count < 0 && !isGameOver())
			Dead.text = "Cliquez pour réapparaitre";
		else if(isGameOver())
			Dead.text = "GAME OVER";
		
		if(Mort && Input.GetMouseButton(0) && count <= 0 && !isGameOver()){
			var theBarRectTransform = furryScript.BarreDeFurry.transform as RectTransform;
			theBarRectTransform.sizeDelta = new Vector2(0, theBarRectTransform.sizeDelta.y);
			//furryScript.animator.SetFloat("Attaque Couteau", 0f); //TODO animator inexistant ?
			furryScript.AnimationWaitEnd = 0;
			attaque.enabled = true;
			furryScript.AnimBaguetteVersCouteau = false;
			furryScript.AnimCouteauVersBaguette = false;
			attaque.furry = 0;
			weaponchange.BlockLeChangementDArme = false;
			furryScript.AmenoPlayer.enabled = true;
			baguette.enabled = false;

			Canvas.SetActive(true);
			pistolet.SetActive(true);
			fusil.SetActive(false);
			mitrailleur1.SetActive(false);
			mitrailleur2.SetActive(false);
			bazooka.SetActive(false);
			Couteau.SetActive(false);
			Loupe.SetActive(false);
			PistoletLaser.SetActive (false);
			PV = PVMax;
			
			Mort = false;
			Dead.text = "";
		}
		
		count -= Time.deltaTime;
    }
	
	bool isGameOver(){
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		bool arePlayersDead = true; 
		
		foreach(GameObject player in players){
			if(!player.GetComponent<PointDeVieJoueur>().Mort)
				arePlayersDead = false;
		}
		
		if(players == null || players.Length == 0)
			return Mort;
		
		return Mort && arePlayersDead;
	}

    void OnTriggerEnter(Collider other)
    {
		if (!isLocalPlayer)
		{
			return;
		}
        if (other.gameObject.tag == "Vache" && FullOpaque == false)
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
		if (!isLocalPlayer)
		{
			return;
		}
        var theBarRectTransform = furryScript.BarreDeFurry.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(0, theBarRectTransform.sizeDelta.y);
        //furryScript.animator.SetFloat("Attaque Couteau", 0f); //TODO animator inexistant ?
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
        Loupe.SetActive(false);
		PistoletLaser.SetActive (false);

		count = tpsRespawn;
        Mort = true;
    }

    public void Appuyé()
    {
		if(isServer)
			NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
		else{
			Mort = false;
			weaponchange.BlockLeChangementDArme = false;
			furryScript.AmenoPlayer.enabled = true;
			
			if(isServer){
				weaponchange.RpcWSetPistoletActive();
			}else{
				weaponchange.CmdWSetPistoletActive();
			}
			
			PV = PVMax;
			transform.position = GameObject.FindWithTag("SpawnPt").transform.position;
		}
    }
}
