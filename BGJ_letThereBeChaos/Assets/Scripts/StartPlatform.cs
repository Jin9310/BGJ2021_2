using UnityEngine;

public class StartPlatform : MonoBehaviour
{

    void Update()
    {
        
        {
            transform.Translate(Vector2.up * 1f * Time.deltaTime);
        }
    }
}
