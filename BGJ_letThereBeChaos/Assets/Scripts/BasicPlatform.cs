using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField] private int _platformSpeed;
    private float _endYPos = 5.25f;

    private void Start()
    {
        _platformSpeed = Random.Range(1,5);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * _platformSpeed * Time.deltaTime);
        KillMe();
    }

    private void KillMe()
    {
        if(transform.position.y >= _endYPos)
        {
            Destroy(gameObject);
        }
    }

}
