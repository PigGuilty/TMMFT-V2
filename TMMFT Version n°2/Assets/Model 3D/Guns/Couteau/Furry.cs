using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class Furry : NetworkBehaviour {

    private Attaque attaque;
    private Baguette baguette;
    private WeaponChange weaponchange;

    public Animator animator;

    private float AnimationLengthCouteau;
    private float AnimationLengthBaguette;
    public float AnimationWaitEnd;

    public bool AnimCouteauVersBaguette;
    public bool AnimBaguetteVersCouteau;
    public bool BlockLeChangementDArme;

    public GameObject BarreDeFurry;

    public AudioSource AmenoPlayer;

	private GameObject Player;
	
    private void Awake()
    {
		Player = GameObject.FindWithTag("Player");
		
		if (!isLocalPlayer)
		{
			return;
		}
        attaque = GetComponent<Attaque>();
        baguette = GetComponent<Baguette>();
        weaponchange = Player.GetComponent<WeaponChange>();
    }

    // Use this for initialization
    void Start () {
		if (!isLocalPlayer)
		{
			return;
		}
        animator = GetComponent<Animator>();
        AnimCouteauVersBaguette = false;
        AnimBaguetteVersCouteau = false;
        AnimationLengthCouteau = 3.33f;
        AnimationLengthBaguette = 3.33f;
        AnimationWaitEnd = 0;
        baguette.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!isLocalPlayer)
		{
			return;
		}
		 var theBarRectTransform = BarreDeFurry.transform as RectTransform;
		 theBarRectTransform.sizeDelta = new Vector2(attaque.furry, theBarRectTransform.sizeDelta.y);

			if (attaque.furry >= 100)
			{
				attaque.furry = 100;
				attaque.enabled = false;
				AnimCouteauVersBaguette = true;
				if (AnimCouteauVersBaguette == true)
				{
					if (AnimationWaitEnd == 0)
					{
						animator.Rebind();
						animator.SetFloat("Attaque Couteau", 0.5f);

						weaponchange.BlockLeChangementDArme = true;

						AmenoPlayer.enabled = true;
						AmenoPlayer.Play();
					}

					AnimationWaitEnd += Time.deltaTime;

					//attendre fin de l'annimation
					if (AnimationWaitEnd >= AnimationLengthCouteau)
					{
						animator.SetFloat("Attaque Couteau", 0.75f);
						AnimationWaitEnd = 0;
						baguette.enabled = true;
						AnimCouteauVersBaguette = false;
						attaque.furry = 99;
					}

				}
			}

			else if (attaque.furry <= -1)
			{
				AnimBaguetteVersCouteau = true;
				if (AnimBaguetteVersCouteau == true)
				{
					if (AnimationWaitEnd == 0)
					{
						baguette.enabled = false;

						animator.Rebind();
						animator.SetFloat("Attaque Couteau", 1f);

						StartCoroutine(FadeOut(AmenoPlayer, 5f));
					}

					AnimationWaitEnd += Time.deltaTime;

					//attendre fin de l'annimation
					if (AnimationWaitEnd >= AnimationLengthBaguette)
					{
						animator.SetFloat("Attaque Couteau", 0f);
						AnimationWaitEnd = 0;
						attaque.enabled = true;
						AnimBaguetteVersCouteau = false;
						attaque.furry = 0;
						weaponchange.BlockLeChangementDArme = false;
					}
				}
			}
     }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        audioSource.enabled = false;
    }

    private void OnEnable()
    {      
        AmenoPlayer.enabled = false;
    }
}
