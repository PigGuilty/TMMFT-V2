using UnityEngine;

public class Shoot : MonoBehaviour {

    // Use this for initialization
    public float DegatArme;

    private Camera fpsCam;
    public ParticleSystem Tire;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;
    public GameObject balle;

	void Start() {
		fpsCam = Camera.main;
	}

    // Update is called once per frame
    void Update () {

        Vector3 lookRot = fpsCam.transform.forward;

        if (Input.GetButtonDown("Fire1"))
        {

            Tire.Play();

            RaycastHit hit;
            Ray ShootingDirection = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

            if (Physics.Raycast(ShootingDirection, out hit))
            {

                Target target = hit.transform.GetComponent<Target>();

                if (hit.collider.tag == "Decor interractif")
                {
                    hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
                }

                if (hit.collider.tag == "Vache")
                {
                    target.TakeDamage(DegatArme);


                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * DegatArme * 4);
                    }
                }

                if (hit.collider.tag != "Player" && hit.collider.tag != "Vache" && hit.collider.tag != "Decor interractif")
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
                    GameObject balleGO = Instantiate(balle, hit.point, Quaternion.LookRotation(lookRot));
                    Destroy(balleGO, 10f);
                }
            }
        }
    }
}
