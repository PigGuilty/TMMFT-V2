using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerByButtun : MonoBehaviour {

    public GameObject EnnemiPrefab;

    public void Appuyé()
    {
        GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
        EnnemiPrefabInstanciate.transform.position = transform.position;
    }

    public void Relaché()
    {
        GameObject EnnemiPrefabInstanciate = Instantiate(EnnemiPrefab, transform);
        EnnemiPrefabInstanciate.transform.position = transform.position;
    }
}
