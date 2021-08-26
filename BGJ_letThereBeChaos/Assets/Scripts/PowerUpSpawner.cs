using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject doubleJump;

    [SerializeField] private float randomXPosition;
    [SerializeField] private float randomYPosition;
    private float randomSpawnTime;

    [SerializeField] private bool readyToSpawn = false;
    private float time = 10f;

    private void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            readyToSpawn = true;
            time = 987456987f * 987456987f;
        }

        randomSpawnTime = Random.Range(8f, 12f);
        randomXPosition = Random.Range(-5, 5);
        randomYPosition = Random.Range(-3, 3);
        if (readyToSpawn == true)
        {
            StartCoroutine(SpawnDoubleJump());
        }
        
    }

    IEnumerator SpawnDoubleJump()
    {
        readyToSpawn = false;
        Instantiate(doubleJump, new Vector2(randomXPosition, randomYPosition), Quaternion.identity);
        yield return new WaitForSeconds(randomSpawnTime);
        readyToSpawn = true;
    }

}
