using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SchtonkNOTTrigger : MonoBehaviour
{
    public AudioClip schtonk;
    private AudioSource Audio;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Vache")
        {
            Audio = gameObject.GetComponent<AudioSource>();
            Audio.clip = schtonk;
            Audio.Play();
        }
    }
}
