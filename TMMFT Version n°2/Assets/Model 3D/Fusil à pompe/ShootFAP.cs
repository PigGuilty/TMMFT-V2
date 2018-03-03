using UnityEngine;

public class ShootFAP : MonoBehaviour
{

    // Use this for initialization
    public float DegatArme;
    public int TailleChargeur;
    public int BalleRestante;

    public Camera fpsCam;
    public ParticleSystem Tire;
    public GameObject impactEffect;
    public GameObject balle;

    Animator animator;
    public Animation anim;

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;

        AnimationClip clip;
    }

    // Update is called once per frame
    void Update()
    {
        print(BalleRestante);

        Vector3 lookRot = fpsCam.transform.forward;

        if (BalleRestante != 0)
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

                        GameObject balleGO = Instantiate(balle, hit.point, Quaternion.LookRotation(lookRot));

                        Destroy(impactGO, 2f);
                        Destroy(balleGO, 10f);
                    }
                }

                BalleRestante = BalleRestante - 1;

            }
        }

        if (BalleRestante == 0)
        {
            animator.SetFloat("Reload", 1);

            //attendre fin de l'annimation
            animator.SetFloat("Reload", 1);
            BalleRestante = TailleChargeur;
        }
    }
}
