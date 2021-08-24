using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
    private float timer = .5f;


    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
