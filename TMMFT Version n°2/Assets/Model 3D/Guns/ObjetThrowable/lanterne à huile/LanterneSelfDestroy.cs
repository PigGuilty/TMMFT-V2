using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanterneSelfDestroy : MonoBehaviour {

    public float DuréeDeVie;
    PutInFire putinfire;
    private bool starting;
    private float increaser;

    // Use this for initialization
    void Start () {
        putinfire = gameObject.GetComponent<PutInFire>();
        starting = true;
        increaser = 0;
    }

	private void Update()
    {
        if(starting == true)
        {
            increaser += Time.deltaTime;

            if(increaser >= DuréeDeVie)
            {
                starting = false;
                putinfire.Desactivé = true;
                increaser = 0;
            }
        }

        if(putinfire.GoDestruction == true)
        {
            increaser += Time.deltaTime;

            if (increaser >= putinfire.TempsPourArreterDeBruler + 0.5f)
            {
                Destroy(gameObject);
                increaser = 0;
            }
            
        }
    }

    private IEnumerable Destruction()
    {
        yield return new WaitForSeconds(putinfire.TempsPourArreterDeBruler + 0.5f);
        Destroy(gameObject);
    }
}
