
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] soundCollection;
    public AudioClip[] soundCollection2;
    public AudioClip[] soundCollection3;
    //public AudioClip[] missingSoundCollection;
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
        }else if(lm.thirdStageOfChaos == true && lm.finalStage == false)
        {
            if (!audioSource.isPlaying)
            {
                rand = Random.Range(0, soundCollection2.Length);
                audioSource.clip = soundCollection2[rand];
                audioSource.Play();
            }
        }
        else if (lm.finalStage == true || lm.fistStageOfChaos == true)
        {
            if (!audioSource.isPlaying)
            {
                rand = Random.Range(0, soundCollection3.Length);
                audioSource.clip = soundCollection3[rand];
                audioSource.Play();
            }
        }
    }
}
