using UnityEngine;

public class DustParticle : MonoBehaviour
{
    private float timer = 1.5f;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
