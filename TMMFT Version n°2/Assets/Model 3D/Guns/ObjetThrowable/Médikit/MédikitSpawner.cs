using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MédikitSpawner : MonoBehaviour
{

    public int SpawnSpeed;
    public GameObject Médikit;
    private float Counter;

    private 

    // Use this for initialization
    void Start()
    {
        Counter = SpawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Médikit.activeSelf == false)
        {
            Counter -= Time.deltaTime;

            if (Counter <= 0)
            {
                Counter = SpawnSpeed;

                Médikit.SetActive(true);
            }
        }
    }
}
