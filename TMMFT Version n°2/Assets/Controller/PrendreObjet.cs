using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendreObjet : MonoBehaviour
{

    public float PortéePourPrendreObjet;

    public GameObject PlacePourObjet;

    public GameObject pistolet;
    public GameObject fusil;
    public GameObject mitrailleur1;
    public GameObject mitrailleur2;
    public GameObject bazooka;
    public GameObject Couteau;

    private bool ObjetPris;

    // Use this for initialization
    void Start()
    {
        ObjetPris = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && ObjetPris == false)
        {
            RaycastHit hit;
            Ray ShootingDirection = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ShootingDirection, out hit, PortéePourPrendreObjet))
            {
                Transform PositionObjet = hit.transform.GetComponent<Transform>();
                Rigidbody rigidbody = hit.transform.GetComponent<Rigidbody>();

                if (hit.collider.tag == "Objet")
                {
                    ObjetPris = true;
                    pistolet.SetActive(false);
                    fusil.SetActive(false);
                    mitrailleur1.SetActive(false);
                    mitrailleur2.SetActive(false);
                    bazooka.SetActive(false);
                    Couteau.SetActive(false);
                    PositionObjet.transform.rotation = PlacePourObjet.transform.rotation;
                    PositionObjet.transform.position = PlacePourObjet.transform.position;
                    rigidbody.isKinematic = true;
                    PositionObjet.transform.parent = PlacePourObjet.transform;
                }
            }
        }
    }
}
