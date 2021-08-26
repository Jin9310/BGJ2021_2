using UnityEngine;

public class DoubleJumpPwrUp : MonoBehaviour
{

    private float selfDestroyTimer = 6f;

    private void Update()
    {
        selfDestroyTimer -= Time.deltaTime;
        if(selfDestroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
