using UnityEngine;

public class ShootFAP : MonoBehaviour
{

    // Use this for initialization
    public float DegatArme;
    public int TailleChargeur;
    private int BalleRestante;

    public Camera fpsCam;
    public ParticleSystem Tire;
    public GameObject impactEffect;
    public GameObject balle;
    public GameObject SpawnBullet;

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

        Vector3 lookRot = fpsCam.transform.forward;

        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {

                Tire.Play();

                RaycastHit hit;
                Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

                if (Physics.Raycast(ShootingDirection, out hit))
                {

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
            }

            AnimationWaitEnd = AnimationWaitEnd + 1;

            if (AnimationWaitEnd == 10)
            {
                Vector3 lookrot2 = new Vector3(lookRot.x + Random.Range(-0.4f, 0.4f), lookRot.y + Random.Range(-0.4f, 0.4f), lookRot.z + Random.Range(-0.4f, 0.4f));

                GameObject balleGO = Instantiate(balle, SpawnBullet.transform.position, Quaternion.LookRotation(lookrot2));
                Destroy(balleGO, 10f);
            }
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
