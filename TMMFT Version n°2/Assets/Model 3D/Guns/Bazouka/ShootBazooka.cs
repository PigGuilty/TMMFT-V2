using UnityEngine;

public class ShootBazooka : MonoBehaviour
{

    // Use this for initialization
    public float DegatArme;
    public int TailleChargeur;
    private int BalleRestante;

    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;

    private Animator animator;

    private int AnimationLength;
    private int AnimationWaitEnd;

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 250;
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

                RaycastHit hit;
                Ray ShootingDirection = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

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

                    if (hit.collider.tag != "Player" && hit.collider.tag != "Vache")
                    {
                        float x = Random.Range(0.0f, 4.0f);
                        if (x <= 1)
                        {
                            GameObject impactGO1 = Instantiate(impactEffect1, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO1.transform.Translate(hit.normal / 1000, Space.World);
                            Destroy(impactGO1, 20f);
                        }
                        else if (x > 1 && x <= 2)
                        {
                            GameObject impactGO2 = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO2.transform.Translate(hit.normal / 1000, Space.World);
                            Destroy(impactGO2, 20f);
                        }
                        else if (x > 2 && x <= 3)
                        {
                            GameObject impactGO3 = Instantiate(impactEffect3, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO3.transform.Translate(hit.normal / 1000, Space.World);
                            Destroy(impactGO3, 20f);
                        }
                        else if (x > 3 && x <= 4)
                        {
                            GameObject impactGO4 = Instantiate(impactEffect4, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO4.transform.Translate(hit.normal / 1000, Space.World);
                            Destroy(impactGO4, 20f);
                        }
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
