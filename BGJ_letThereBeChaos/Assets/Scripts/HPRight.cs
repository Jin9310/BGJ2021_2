using System.Collections;
using UnityEngine;

public class HPRight : MonoBehaviour
{
    private int _platformSpeed;
    private float _rightZPos = 11f;

    private void Start()
    {
        _platformSpeed = Random.Range(1, 5);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _platformSpeed * Time.deltaTime);
        KillMe();
    }

    private void KillMe()
    {
        if (transform.position.x >= _rightZPos)
        {
            Destroy(gameObject);
        }
    }
}
