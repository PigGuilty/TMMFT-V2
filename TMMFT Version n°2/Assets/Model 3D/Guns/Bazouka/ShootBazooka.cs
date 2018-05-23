using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ShootBazooka : MonoBehaviour
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

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 4.17f;
        AnimationWaitEnd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookRot = Camera.main.transform.forward;


        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(Missile, PointDeDépartDuMissile.transform.position , Quaternion.LookRotation(-lookRot));
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
