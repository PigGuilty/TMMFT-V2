using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerByButtun : MonoBehaviour {

    public GameObject EnnemiPrefab;

    public void Appuyé()
    {
        Instantiate(EnnemiPrefab, transform);
    }

    public void Relaché()
    {
        Instantiate(EnnemiPrefab, transform);
    }
}
