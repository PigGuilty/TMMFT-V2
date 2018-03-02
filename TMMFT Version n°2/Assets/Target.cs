using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Target : MonoBehaviour {

    // Use this for initialization

    public Color OriginalColor;

    public float Vie;

    public ParticleSystem mourir;

    [SerializeField] AudioClip aie;
    [SerializeField] AudioClip meurt;

    public void Start()
    {
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        material.color = Color.red;
        GetComponent<Renderer>().material = material;
    }


    public void TakeDamage (float amount)
    {
        AudioSource audio = GetComponent<AudioSource>();

        Vie -= amount;

        if(Vie <= 0)
        {            
            audio.clip = meurt;
            audio.Play();
            mourir.Play();
            Destroy(gameObject);
        }

        else
        {
            audio.clip = aie;
            audio.Play();
            Material material = new Material(Shader.Find("Transparent/Diffuse"));
            material.color = Color.red;
            GetComponent<Renderer>().material = material;
        }

    }


    public void Update()
    {
        Material material = new Material(Shader.Find("Transparent/Diffuse"));

        if (this.gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            print("OUIIII");

            material.color = OriginalColor;
            GetComponent<Renderer>().material = material;
        }
    }

}
	
