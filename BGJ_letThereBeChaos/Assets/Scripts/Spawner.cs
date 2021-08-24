using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] platform;
    private float timer;
    public float startTimer;

    public LevelManager lm;

    //this is being set here so I can re-use the script for horzintal and vertical spawners without need to recreate separate one for each
    //**************************************** here are the values that should always work well *********
    [SerializeField] private float xCoordMin; // -7
    [SerializeField] private float xCoordMax; // +7
    [SerializeField] private float yCoordMin; // -10
    [SerializeField] private float yCoordMax; // 10


    private void Update()
    {
        if(lm.startGame != false)
        {
            if (timer > 0)
            { timer -= Time.deltaTime; }
            else
            {
                int rand = Random.Range(0, platform.Length);

                //example
                //Vector3 randomPosition = new Vector3(Random.Range(-7,7), Random.Range(-7, -7));
                //example
                //Vector3 randomPosition = new Vector3(Random.Range(-10,10), Random.Range(-10, -10));
                Vector3 randomPosition = new Vector3(Random.Range(xCoordMin, xCoordMax), Random.Range(yCoordMin, yCoordMax));
                Instantiate(platform[rand], randomPosition, Quaternion.identity);
                timer = startTimer;
            }
        }
        
    }
}
