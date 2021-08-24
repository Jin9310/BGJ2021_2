using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private int _platformSpeed;
    private float _endYPos = 5.25f;
    private float _lowPos = -10f;

    [SerializeField] private bool _beginTheDescent = false;

    private void Start()
    {
        _platformSpeed = Random.Range(1, 5);
    }

    private void Update()
    {

        if(_beginTheDescent == true)
        {
            StartCoroutine(Fall());
        }else
        {
            transform.Translate(Vector2.up * _platformSpeed * Time.deltaTime);
        }
    }

    private void KillMe()
    {
        if (transform.position.y >= _endYPos || transform.position.y <= _lowPos)
        {
            Destroy(gameObject);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            _beginTheDescent = true;
    }

    IEnumerator Fall()
    {
        
        yield return new WaitForSeconds(1f);
        transform.Translate(Vector2.up * -5f * Time.deltaTime);
        KillMe();
    }
}
