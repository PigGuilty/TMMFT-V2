using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assièteBreking : MonoBehaviour {

    public GameObject Assiète;
    public GameObject AssièteCassé;

    public Collider col;

    public GameObject Player;
    private PrendreObjet prendreobjet;

    private bool OnALaDir;

    public static Vector3 dir;

    public GameObject JoueurDeSon;

    // Use this for initialization
    void Start () {
        OnALaDir = false;
        col = Assiète.GetComponent<Collider>();
        prendreobjet = Player.GetComponent<PrendreObjet>();
    }

    private void Update()
    {
        if(OnALaDir == false)
        {
            if(col.enabled == true)
            {
                dir = new Vector3(Camera.main.transform.forward.x, 0.05f, Camera.main.transform.forward.z);
                OnALaDir = true;
                print("Dir" + dir);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (prendreobjet.ObjetPris == false)
        {
            GameObject Assièteinvoqué = Instantiate(AssièteCassé);
            Assièteinvoqué.transform.position = Assiète.transform.position;

            Instantiate(JoueurDeSon);

            Destroy(Assiète);
        }
    }
}
