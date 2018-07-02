using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class Target : NetworkBehaviour {

    // Use this for initialization
	
	[SyncVar]
    public float Vie;

	public float VieMax;

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

	public bool IsVacheBipede = false;
	public bool IsMeuleDeFromage = false;

	public GameObject ExplosionMeule;

	private Animator animator;

    public void Start()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;
        mats[1] = Origine;
        //mats[7] = Origine;
        rend.materials = mats;

		if (IsMeuleDeFromage == true) {
			Trigger = GetComponentInChildren<Transform> ().GetComponentInChildren<Transform> ().GetComponentInChildren<Collider> ();
		} else {
			Trigger = GetComponent<Collider> ();
		}
        waiting = 0;

		ScoreTextObject = GameObject.FindWithTag ("localScore");
		ScoreText = ScoreTextObject.GetComponent<Text> ();
		scoreEnText = ScoreText.text.Replace ("Score : ", "");
		score = int.Parse (scoreEnText);

		Vie = VieMax;

		if (IsMeuleDeFromage == true) {
			animator = GetComponentInChildren<Animator> ();
		} else {
			animator = GetComponent<Animator> ();
		}
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

			score++; //Nieh è_é
			ScoreText.text = "Score : " + score.ToString ();

			if (IsVacheBipede == true) {
				animator.Rebind ();
				animator.SetFloat ("Blend", 1.5f);
				gameObject.GetComponent<NavMeshAgent> ().speed = 0.0f;
				Destroy (gameObject, 1.2f);
			} else if (IsMeuleDeFromage == true) {
				GameObject ExplosionMeuleGO = Instantiate (ExplosionMeule);
				ExplosionMeuleGO.transform.position = gameObject.transform.position;
				Destroy (gameObject);
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
            mats[1] = Red;
            //mats[7] = Red;
            rend.materials = mats;

			if (IsVacheBipede == true) {
				StartCoroutine("FeelPain");
			}
			if (IsMeuleDeFromage == true) {
				animator.SetFloat ("Blend", Mathf.Abs ((Vie / VieMax) - 1));
			}
        }

    }


    public void Update()
    {
		if (IsVacheBipede == true) {			
			SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer> ();
			Material[] mats = rend.materials;
			if (mats [0].color == Red.color) {//[indexOfColor1]
				if (waiting >= 2) {
					mats [0] = Origine;//[indexOfColor1]
					mats [0] = Origine;//[indexOfColor2]
					rend.materials = mats;
					waiting = 0;
				}
			
				if (waiting < 2) {
					waiting++;
				}
			}
		}
	}

	IEnumerator FeelPain(){
		float floatOfAnim = Mathf.Abs ((Vie / VieMax) - 1);
		animator.SetFloat ("Blend", floatOfAnim + 2.0f);
		yield return new WaitForSeconds (0.5f);
		if (Vie > 0) {
			animator.SetFloat ("Blend", floatOfAnim);
		}
	}
}
	
