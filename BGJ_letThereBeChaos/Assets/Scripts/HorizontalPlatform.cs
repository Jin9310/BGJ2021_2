using System.Collections;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    private int _platformSpeed;
    private float _leftZPos = -11f;

    private void Start()
    {
        _platformSpeed = Random.Range(1, 5);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * _platformSpeed * Time.deltaTime);
        KillMe();
    }

    private void KillMe()
    {
        if (transform.position.x <= _leftZPos)
        {
            Destroy(gameObject);
        }
    }
}
