using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawnPoint : MonoBehaviour
{
    public GameObject[] box;
    public LevelManager lm;

    private float timer;
    [SerializeField] private float timerStartTime = 3f;
    private float fasterSpawn;

    private void Start()
    {
        timer = timerStartTime;
        fasterSpawn = timerStartTime / 2;
    }
    private void Update()
    {
        if(lm.thirdStageOfChaos != true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                int rand = Random.Range(0, box.Length);
                Instantiate(box[rand], transform.position, Quaternion.identity);
                timer = timerStartTime;
            }
        }else
        {
            fasterSpawn -= Time.deltaTime;
            if (fasterSpawn <= 0)
            {
                int rand = Random.Range(0, box.Length);
                Instantiate(box[rand], transform.position, Quaternion.identity);
                fasterSpawn = timerStartTime/2;
            }
        }
        
    }

}
