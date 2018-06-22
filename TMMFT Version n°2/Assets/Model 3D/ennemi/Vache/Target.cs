using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class Target : MonoBehaviour {

    // Use this for initialization

	public float Vie;
	public float VieMax;

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

	public bool IsVacheBipede = false;

	private Animator animator;

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

		Vie = VieMax;

		animator = GetComponent<Animator> ();
    }


    public void TakeDamage (float amount, bool PlaySound)
    {

        AudioSource audio = GetComponent<AudioSource>();

        Vie -= amount;

		if (Vie <= 0) {
			Trigger.enabled = false;
			gameObject.layer = 2;

			audio.clip = meurt;
			audio.Play ();
			mourir.Play ();

			score++;
			ScoreText.text = "Score : " + score.ToString ();

			if (IsVacheBipede == true) {
				animator.Rebind ();
				animator.SetFloat ("Blend", 1);
				gameObject.GetComponent<NavMeshAgent> ().speed = 0.0f;
				Destroy (gameObject, 1.2f);
			} else {
				Destroy (gameObject, audio.clip.length);
			}
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

			if (IsVacheBipede == true) {
				GetComponentInChildren<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, 100.0f);
				GetComponentInChildren<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, Mathf.Abs ((Vie / VieMax * 100) - 100));
			}
        }

    }


    public void Update()
	{
		if (IsVacheBipede == true) {
			
		}

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
	
