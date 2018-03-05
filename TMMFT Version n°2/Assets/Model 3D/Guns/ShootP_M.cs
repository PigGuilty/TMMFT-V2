using UnityEngine;

public class ShootP_M : MonoBehaviour
{

    public float DegatArme;
    public int TailleChargeur;
	public int shootPerFrame;
	public string button;
	private int Counter;
    private int BalleRestante;

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

				RaycastHit hit;
				Ray ShootingDirection = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

				if (Physics.Raycast (ShootingDirection, out hit)) {

					Target target = hit.transform.GetComponent<Target> ();

					if (hit.collider.tag == "Vache") {
						target.TakeDamage (DegatArme);

						if (hit.rigidbody != null) {
							hit.rigidbody.AddForce (-hit.normal * DegatArme * 10);
						}
					}

					if (hit.collider.tag != "Player") {
						GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));

						Destroy (impactGO, 2f);
					}
				}

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
