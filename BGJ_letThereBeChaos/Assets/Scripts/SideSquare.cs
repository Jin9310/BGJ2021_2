using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSquare : MonoBehaviour
{
    private float pushForce = 500f;
    Rigidbody2D rb;
    [SerializeField] private float direction;

    public GameObject puffEffect;

    private float timer;
    private float setRandomTimer;
    

    void Start()
    {
        setRandomTimer = Random.Range(3f, 6f);
        timer = setRandomTimer;

        rb = GetComponent<Rigidbody2D>();
        //direction = -1;
        rb.AddForce(transform.right * pushForce * direction);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer > 1)
        {
            rb.gravityScale = 1f;
        }

        if(timer <= 0)
        {
            Instantiate(puffEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
