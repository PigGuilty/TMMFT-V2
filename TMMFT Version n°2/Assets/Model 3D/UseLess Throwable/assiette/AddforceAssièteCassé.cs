using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AddforceAssièteCassé : MonoBehaviour {

    public assièteBreking assièteBreking;

    private int RandomNumber;
    private int increase;

    private int SelectionOfSound;

    private AudioSource Audio;

    public AudioClip Son0;
    public AudioClip Son1;
    public AudioClip Son2;
    public AudioClip Son3;

    private bool AutorizéAJouerDuSon;
    private int NombreDeFoisQuilAJoueLeSon;

    // Use this for initialization
    void Start () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        assièteBreking.GetComponent<assièteBreking>();
        Audio = gameObject.GetComponent<AudioSource>();

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

        if (NombreDeFoisQuilAJoueLeSon >= 10)
        {
            AutorizéAJouerDuSon = false;
            Audio.Stop();
            Audio.enabled = false;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(AutorizéAJouerDuSon == true && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Vache" )
        {
            if (NombreDeFoisQuilAJoueLeSon == 0)
            {
                if (SelectionOfSound == 0)
                {
                    Audio.clip = Son0;
                }
                else if (SelectionOfSound == 1)
                {
                    Audio.clip = Son1;
                }
                else if (SelectionOfSound == 2)
                {
                    Audio.clip = Son2;
                }
                else if (SelectionOfSound == 3)
                {
                    Audio.clip = Son3;
                }
            }
            Audio.Play();
            NombreDeFoisQuilAJoueLeSon++;
        }
    }
}
