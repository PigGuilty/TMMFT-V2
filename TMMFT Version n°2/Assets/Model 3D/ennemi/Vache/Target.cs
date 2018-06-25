using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]

public class Target : NetworkBehaviour {

    // Use this for initialization
	
	[SyncVar]
    public float Vie;

    public ParticleSystem mourir;

    [SerializeField] AudioClip aie;
    [SerializeField] AudioClip meurt;

    private Collider Trigger;

    public Material Red;
    public Material Origine;

    private int waiting;

	private int score;
	private GameObject ScoreTextObject;
	private Text ScoreText;
	private string scoreEnText;

    public void Start()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;
        mats[1] = Origine;
        mats[7] = Origine;
        rend.materials = mats;

        Trigger = GetComponent<Collider>();

        waiting = 0;

		ScoreTextObject = GameObject.Find ("Score");
		ScoreText = ScoreTextObject.GetComponent<Text> ();
		scoreEnText = ScoreText.text.Replace ("Score : ", "");
		score = int.Parse (scoreEnText);
    }


    public void TakeDamage (float amount, bool PlaySound)
    {

        AudioSource audio = GetComponent<AudioSource>();

        Vie -= amount;

        if(Vie <= 0)
        {
            Trigger.enabled = false;
			gameObject.layer = 2;

            audio.clip = meurt;
            audio.Play();
            mourir.Play();

			score++;
			ScoreText.text = "Score : " + score.ToString ();

			Destroy(gameObject, audio.clip.length);
        }

        else
        {
            if(PlaySound == true)
            {
                audio.clip = aie;
                audio.Play();
            }
            SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] mats = rend.materials;
            mats[1] = Red;
            mats[7] = Red;
            rend.materials = mats;
        }

    }


    public void Update()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;

        if (mats[1].color == Red.color)
        {
            if (waiting >= 2)
            {
                mats[1] = Origine;
                mats[7] = Origine;
                rend.materials = mats;
                waiting = 0;
            }

            if (waiting < 2)
            {
                waiting++;
            }

        }
    }
}
	
