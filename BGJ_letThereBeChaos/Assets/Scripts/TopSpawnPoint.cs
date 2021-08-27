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

    private ShakeCamera shake;


    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ShakeCamera>();
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
                shake.CamShake();
                timer = timerStartTime;
            }
        }else
        {
            fasterSpawn -= Time.deltaTime;
            if (fasterSpawn <= 0)
            {
                int rand = Random.Range(0, box.Length);
                Instantiate(box[rand], transform.position, Quaternion.identity);
                shake.CamShake();
                fasterSpawn = timerStartTime/2;
            }
        }
        
    }

}
