using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assièteBreking : MonoBehaviour {

    public GameObject Assiète;
    public GameObject AssièteCassé;

    public Collider col;

    private bool OnALaDir;

    public static Vector3 dir;

	// Use this for initialization
	void Start () {
        OnALaDir = false;
        col = Assiète.GetComponent<Collider>();
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
        GameObject Assièteinvoqué = Instantiate(AssièteCassé);
        Assièteinvoqué.transform.position = Assiète.transform.position;
        Destroy(Assiète);
    }
}
