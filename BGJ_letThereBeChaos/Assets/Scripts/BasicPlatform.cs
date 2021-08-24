using System.Collections;
using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField] private int _platformSpeed;
    private float _endYPos = 5.25f;

    Animator anim;

    public LevelManager lm;

    private void Start()
    {
        anim = GetComponent<Animator>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lm.keepScoreCount++;
        StartCoroutine(TouchMe());
    }

    IEnumerator TouchMe()
    {
        anim.SetBool("touched", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("touched", false);
    }

}
