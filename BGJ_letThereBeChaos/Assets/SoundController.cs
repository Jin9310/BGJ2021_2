
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] soundCollection;
    public int rand;

    public LevelManager lm;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {


        if(lm.secondStageOfChaos == true && lm.finalStage == false)
        {
            if (!audioSource.isPlaying)
            {
                rand = Random.Range(0, soundCollection.Length);
                audioSource.clip = soundCollection[rand];
                audioSource.Play();
            }
        }  
    }
}
