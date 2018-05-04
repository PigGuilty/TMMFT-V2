using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppuisRespawn : MonoBehaviour
{
    private GameObject ObjetAvecLequelIlYAInterraction;

    public void HitByRaycast()
    {
        ObjetAvecLequelIlYAInterraction = GameObject.FindWithTag("Player");

        ObjetAvecLequelIlYAInterraction.transform.SendMessage("Appuyé", SendMessageOptions.DontRequireReceiver);
    }
}
