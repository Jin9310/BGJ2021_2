using UnityEngine.SceneManagement;
using UnityEngine;

public class EntryScript : MonoBehaviour
{

    private float timer;
    private float startTimer = 5f;

    void Start()
    {
        timer = startTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        
    }
}
