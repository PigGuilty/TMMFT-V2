using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SchtonkTrigger : MonoBehaviour
{
    public AudioClip schtonk;
    private AudioSource Audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Vache")
        {
            Audio = gameObject.GetComponent<AudioSource>();
            Audio.clip = schtonk;
            Audio.Play();
        }
    }
}
