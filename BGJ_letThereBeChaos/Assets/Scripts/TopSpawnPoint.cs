using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawnPoint : MonoBehaviour
{
    public GameObject box;

    private float timer;
    [SerializeField] private float timerStartTime = 3f;

    private void Start()
    {
        timer = timerStartTime;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(box, transform.position, Quaternion.identity);
            timer = timerStartTime;
        }
    }

}
