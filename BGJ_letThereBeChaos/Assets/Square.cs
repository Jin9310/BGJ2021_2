using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public GameObject puffEffect;

    private float timer;
    private float setRandomTimer;

    void Start()
    {
        setRandomTimer = Random.Range(3f, 6f);
        timer = setRandomTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(puffEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
