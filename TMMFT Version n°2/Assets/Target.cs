using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Target : MonoBehaviour {

    // Use this for initialization

    public float Vie;

    public ParticleSystem mourir;

    [SerializeField] AudioClip aie;
    [SerializeField] AudioClip meurt;

    private Collider Trigger;

    public Material Red;
    public Material Origine;

    private int waiting;

    public void Start()
    {
        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;
        mats[1] = Origine;
        mats[7] = Origine;
        rend.materials = mats;

        Trigger = GetComponent<Collider>();

        waiting = 0;
    }


    public void TakeDamage (float amount)
    {
        AudioSource audio = GetComponent<AudioSource>();

        Vie -= amount;

        if(Vie <= 0)
        {
            Trigger.enabled = false;
            audio.clip = meurt;
            audio.Play();
            mourir.Play();
            Destroy(gameObject, audio.clip.length);
        }

        else
        {
            audio.clip = aie;
            audio.Play();
            SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] mats = rend.materials;
            mats[1] = Red;
            mats[7] = Red;
            rend.materials = mats;
        }

    }


    public void Update()
    {

        SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = rend.materials;

        if (mats[1].color == Red.color)
        {
            if (waiting >= 2)
            {
                mats[1] = Origine;
                mats[7] = Origine;
                rend.materials = mats;
                waiting = 0;
            }

            if (waiting < 2)
            {
                waiting++;
            }

        }
    }
}
	
