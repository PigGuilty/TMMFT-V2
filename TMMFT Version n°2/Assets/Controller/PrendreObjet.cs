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

    private string ObjectName;
    private GameObject ObjectQuiEstPris;

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
                ObjectName = hit.collider.gameObject.name;

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

        if (Input.GetButtonDown("Fire1") && ObjetPris == true)
        {
            //ajouter une force au rigidbody de l'objet... mais on n'y a plus accès :/
            print(ObjectName);
            ObjectQuiEstPris = GameObject.Find(ObjectName);
            Rigidbody rigidbodyDeLObjetPris = ObjectQuiEstPris.transform.GetComponent<Rigidbody>();
            Vector3 DirectionForceJet = Camera.main.transform.forward + new Vector3(0f, 0.4f, 0f);

            Debug.DrawRay(transform.position, DirectionForceJet *4, Color.green);

            rigidbodyDeLObjetPris.isKinematic = false;
            ObjectQuiEstPris.transform.parent = null;
            rigidbodyDeLObjetPris.AddForce(DirectionForceJet * 500);

            ObjetPris = false;
        }
    }
}
