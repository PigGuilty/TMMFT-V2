using UnityEngine;

public class Target : MonoBehaviour {

    // Use this for initialization

    public float Vie;

    public void TakeDamage (float amount)
    {
        Vie -= amount;
        if(Vie <= 0)
        {
            Destroy(gameObject);
        }
    }
	
}
