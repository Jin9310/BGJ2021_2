using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
    private float timer = .5f;
    public GameObject effect;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
