using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppuisRespawn : MonoBehaviour
{
    private GameObject ObjetAvecLequelIlYAInterraction;

    public void HitByRaycast()
    {
        ObjetAvecLequelIlYAInterraction = GameObject.FindWithTag("localPlayer");

        ObjetAvecLequelIlYAInterraction.transform.SendMessage("Appuyé", SendMessageOptions.DontRequireReceiver);
    }
}
