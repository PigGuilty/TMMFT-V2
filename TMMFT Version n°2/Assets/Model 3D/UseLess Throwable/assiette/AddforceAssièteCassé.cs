using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddforceAssièteCassé : MonoBehaviour {

    public assièteBreking assièteBreking;

    private int RandomNumber;
    private int increase;

    private int SelectionOfSound;

    public GameObject JoueurDeSon0;
    public GameObject JoueurDeSon1;
    public GameObject JoueurDeSon2;
    public GameObject JoueurDeSon3;

    private bool AutorizéAJouerDuSon;
    private int NombreDeFoisQuilAJoueLeSon;

    // Use this for initialization
    void Start () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        assièteBreking.GetComponent<assièteBreking>();
        print("assiète" + assièteBreking.dir);
        rb.AddForce(assièteBreking.dir * 500);

        RandomNumber = Random.Range(700, 740);

        SelectionOfSound = Random.Range(0, 4);
        NombreDeFoisQuilAJoueLeSon = 0;
        AutorizéAJouerDuSon = true;
    }

    private void Update()
    {
        if(increase >= RandomNumber)
        {
            Destroy(gameObject);
        }

        increase++;

        if (NombreDeFoisQuilAJoueLeSon >= 20)
        {
            AutorizéAJouerDuSon = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (AutorizéAJouerDuSon == true && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Vache")
        {
            if (SelectionOfSound == 0)
            {
                Instantiate(JoueurDeSon0);
            }
            else if (SelectionOfSound == 1)
            {
                Instantiate(JoueurDeSon1);
            }
            else if (SelectionOfSound == 2)
            {
                Instantiate(JoueurDeSon2);
            }
            else if (SelectionOfSound == 3)
            {
                Instantiate(JoueurDeSon3);
            }
            NombreDeFoisQuilAJoueLeSon++;
        }
    }
}
