using UnityEngine;

public class ShootFAP : MonoBehaviour
{

    // Use this for initialization
    public float DegatArme;
	public float nbProjctiles;
	public float Dispertion;
    public int TailleChargeur;
    private int BalleRestante;

    public ParticleSystem Tire;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;
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

        Vector3 lookRot = Camera.main.transform.forward;

        if (BalleRestante > 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {

                Tire.Play();

                RaycastHit hit;
				for (int i = 0; i < nbProjctiles; i++) {
					Vector3 dir = Camera.main.transform.forward;
					dir.x = dir.x * (Dispertion / 100) + Random.Range (-0.01f, 0.01f);
					dir.y = dir.y * (Dispertion / 100) + Random.Range (-0.01f, 0.01f);
					dir.z = dir.z * (Dispertion / 100) + Random.Range (-0.01f, 0.01f);

					Ray ShootingDirection = new Ray (Camera.main.transform.position, dir);

					Debug.DrawRay(Camera.main.transform.position, dir * 1000000f, Color.red,3);

					if (Physics.Raycast (ShootingDirection, out hit)) {

						Target target = hit.transform.GetComponent<Target> ();

                        if (hit.collider.tag == "Decor interractif")
                        {
                            hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                        }

                        if (hit.collider.tag == "Vache") {
							target.TakeDamage (DegatArme	);

							if (hit.rigidbody != null) {
								hit.rigidbody.AddForce (-hit.normal * DegatArme * 4);
							}
						}

						if (hit.collider.tag != "Player" && hit.collider.tag != "Vache" && hit.collider.tag != "Decor interractif") {
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
