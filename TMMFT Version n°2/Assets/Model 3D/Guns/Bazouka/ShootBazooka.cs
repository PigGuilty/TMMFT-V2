using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]

public class ShootBazooka : NetworkBehaviour
{

    // Use this for initialization
    public int TailleChargeur;
    private int BalleRestante;

    private Animator animator;

    private float AnimationLength;
    private float AnimationWaitEnd;

    public GameObject Missile;
    public GameObject PointDeDépartDuMissile;

    public AudioClip tireBazooka;
	private NetworkSpawner netSpawner;
	public bool m_isServer;
    private Camera fpsCam;
	
    void Start()
    {
		if (gameObject.transform.parent.parent.tag != "localPlayer")
		{
			return;
		}
		m_isServer = gameObject.transform.parent.parent.GetComponent<FirstPersonController>().isServer;
		fpsCam = GameObject.FindWithTag("localCamera").GetComponent<Camera>();
		
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 4.17f;
        AnimationWaitEnd = 0;
		netSpawner = gameObject.transform.parent.parent.GetComponent<NetworkSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
		if (gameObject.transform.parent.parent.tag != "localPlayer")
		{
			return;
		}
        Vector3 lookRot = fpsCam.transform.forward;


        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                if(m_isServer){
					GameObject item = Instantiate(Missile, PointDeDépartDuMissile.transform.position , Quaternion.LookRotation(-lookRot));
					NetworkServer.Spawn(item);
				}else{
					netSpawner.Spawn(Missile, PointDeDépartDuMissile.transform.position, Quaternion.LookRotation(-lookRot), -1, "");
				}
				BalleRestante = BalleRestante - 1;

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = tireBazooka;
                audio.Play();
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

            //attendre fin de l'annimation
            if (AnimationWaitEnd >= AnimationLength)
            {
                animator.SetFloat("Reload", 0);
                BalleRestante = TailleChargeur;
                AnimationWaitEnd = 0;
            }

        }
    }
}
