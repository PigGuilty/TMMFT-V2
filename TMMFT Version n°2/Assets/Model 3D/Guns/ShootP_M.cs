using UnityEngine;

public class ShootP_M : MonoBehaviour
{

    // Use this for initialization
    public float DegatArme;
    public int TailleChargeur;
    private int BalleRestante;

    public Camera fpsCam;
    public ParticleSystem Tire;
    public GameObject impactEffect;

    public GameObject ClonePM;
        
    private Animator animator;

    private int AnimationLength;
    private int AnimationWaitEnd;

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 50;
        AnimationWaitEnd = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 lookRot = -fpsCam.transform.right;

        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {

                Tire.Play();

                RaycastHit hit;
                Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

                if (Physics.Raycast(ShootingDirection, out hit))
                {
                    Debug.Log(hit.transform.name);

                    Target target = hit.transform.GetComponent<Target>();

                    if (hit.collider.tag == "Vache")
                    {
                        target.TakeDamage(DegatArme);

                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * DegatArme * 4);
                        }
                    }

                    if (hit.collider.tag != "Player")
                    {
                        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                        Destroy(impactGO, 2f);
                    }
                }

                BalleRestante = BalleRestante - 1;

            }
        }

        if (BalleRestante <= 0)
        {

            if (AnimationWaitEnd == 0)
            {
                animator.Rebind();
                animator.SetFloat("Reload", 1);

                GameObject ClonePMGO = Instantiate(ClonePM, transform.position, Quaternion.LookRotation(lookRot));

                Destroy(ClonePMGO, 10f);
            }

            AnimationWaitEnd = AnimationWaitEnd + 1;

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
