using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Target : MonoBehaviour {

    // Use this for initialization

    public float Vie;

    public ParticleSystem mourir;

    [SerializeField] AudioClip aie;
    [SerializeField] AudioClip meurt;

    private Collider Trigger;

    public Material Red;
    public Material Origine;
	public int indexOfColor1;
	public int indexOfColor2;

    private int waiting;

	private int score;
	private GameObject ScoreTextObject;
	private Text ScoreText;
	private string scoreEnText;

    public void Start()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;
		mats[indexOfColor1] = Origine;
		mats[indexOfColor2] = Origine;
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
			mats[indexOfColor1] = Red;
			mats[indexOfColor2] = Red;
            rend.materials = mats;
        }

    }


    public void Update()
	{
		SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer> ();
		Material[] mats = rend.materials;
		if (mats [indexOfColor1].color == Red.color) {
			if (waiting >= 2) {
				mats [indexOfColor1] = Origine;
				mats [indexOfColor2] = Origine;
				rend.materials = mats;
				waiting = 0;
			}
		
			if (waiting < 2) {
				waiting++;
			}
		}
	}
}
	
