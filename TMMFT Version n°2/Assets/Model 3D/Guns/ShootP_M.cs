using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ShootP_M : MonoBehaviour
{

    public float DegatArme;
    public int TailleChargeur;
	public int shootPerFrame;
	public string button;
	private int Counter;
    private int BalleRestante;

    public ParticleSystem Tire;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;

    public GameObject ClonePM;
        
    private Animator animator;

    private int AnimationLength;
    private int AnimationWaitEnd;

    public GameObject DouilleAInstanciate;
    public GameObject PositionSpawnDouille;

    public AudioClip SonTire;

    void Start()
    {
        animator = GetComponent<Animator>();
        BalleRestante = TailleChargeur;
        AnimationLength = 50;
        AnimationWaitEnd = 0;
		Counter = shootPerFrame;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 lookRot = -Camera.main.transform.right;

        if (BalleRestante > 0)
        {

			if (Input.GetButton (button) && Counter == 0) {

				Tire.Play ();

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.clip = SonTire;
                audio.Play();

                RaycastHit hit;
				Ray ShootingDirection = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

				if (Physics.Raycast (ShootingDirection, out hit)) {

					Target target = hit.transform.GetComponent<Target> ();

                    if (hit.collider.tag == "Decor interractif")
                    {
                        hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                    }

                    if (hit.collider.tag == "Vache") {
						target.TakeDamage (DegatArme,true);

						if (hit.rigidbody != null) {
							hit.rigidbody.AddForce (-hit.normal * DegatArme * 10);
						}
					}

					if (hit.collider.tag != "Player" && hit.collider.tag != "Vache" && hit.collider.tag != "Decor interractif") {
                        float x = Random.Range(0.0f, 4.0f);
                        if ( x <= 1)
                        {
                            GameObject impactGO1 = Instantiate(impactEffect1, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO1.transform.Translate(hit.normal / 1000, Space.World);
                            impactGO1.transform.parent = hit.transform;
                            Destroy(impactGO1, 20f);
                        }
                        else if (x > 1 && x <= 2)
                        {
                            GameObject impactGO2 = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO2.transform.Translate(hit.normal / 1000, Space.World);
                            impactGO2.transform.parent = hit.transform;
                            Destroy(impactGO2, 20f);
                        }
                        else if (x > 2 && x <= 3)
                        {
                            GameObject impactGO3 = Instantiate(impactEffect3, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO3.transform.Translate(hit.normal / 1000, Space.World);
                            impactGO3.transform.parent = hit.transform;
                            Destroy(impactGO3, 20f);
                        }
                        else if (x > 3 && x <= 4)
                        {
                            GameObject impactGO4 = Instantiate(impactEffect4, hit.point, Quaternion.LookRotation(hit.normal));
                            impactGO4.transform.Translate(hit.normal / 1000, Space.World);
                            impactGO4.transform.parent = hit.transform;
                            Destroy(impactGO4, 20f);
                        }
                    }
				}

                GameObject BalleAInstanciateGO = Instantiate(DouilleAInstanciate, PositionSpawnDouille.transform.position, Quaternion.LookRotation(lookRot));
                Destroy(BalleAInstanciateGO, 10f);

                BalleRestante = BalleRestante - 1;

				Counter = shootPerFrame;
			} else if (Input.GetButton ("Fire1")) {
				Counter--;
			} else {
				Counter = 0;
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
