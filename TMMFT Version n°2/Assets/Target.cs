using UnityEngine;

public class Target : MonoBehaviour {

    // Use this for initialization

    public Color blue = new Color(0.2F, 0.0F, 1.0F, 1.0F);

    public float Vie;

    public void TakeDamage (float amount)
    {
        Vie -= amount;

        if(Vie <= 0)
        {
            Destroy(gameObject);
        }

        else
        {        
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

            material.color = blue;
            GetComponent<Renderer>().material = material;
        }
    }

}
	
