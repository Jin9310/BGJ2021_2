using UnityEngine;

public class SingleSpawner : MonoBehaviour
{

    public GameObject basicPlatform;
    private float timer = 2f;

    void Start()
    {
        //spawn the platform
        Instantiate(basicPlatform, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        //destroy the spawner as it is not needed anymore
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

}
