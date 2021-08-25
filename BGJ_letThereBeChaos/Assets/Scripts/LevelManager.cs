using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int jumpCount;

    private int anotherJumpCount;
    private int anotherJumpCountBase = 0;
    private int randomJumpScore;


    //POINTS SCORE
    public float points;
    [SerializeField] private float pointTime;
    private float pointTimeStart = 5;

    public bool startGame = false;

    public GameObject chaosCollection;
    public GameObject textAsGameObject;
    public Text text;


    //ABILITIES CANVAS TEXT
    public GameObject textOfDoubleJump;
    public GameObject textOfDoubleJumpOFF;
    public GameObject textOfDash;
    public GameObject textOfDashOFF;


    public Player pl;

    public GameObject finalFrame;
    public Text finalText;

    public Text scoreText;

    public BasicPlatform basicPlatform;


    //CHAOS
    private bool fistStageOfChaos = false;
    private bool secondStageOfChaos = false;
    private bool thirdStageOfChaos = false;


    private void Start()
    {
        pointTime = pointTimeStart;
        anotherJumpCount = anotherJumpCountBase;
        Time.timeScale = 1;
        StartCoroutine(StartCountDown());
    }

    private void Update()
    {
        
       

        if(!startGame == false)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                jumpCount++;
                anotherJumpCount++;
            }

            if (jumpCount == 10)
            {
                fistStageOfChaos = true;
                StartCoroutine(ChaosOn());
                //enter the chaos
                // enable all spawners
                chaosCollection.gameObject.SetActive(true);
            }
        } 

        if(pl.gotDoubleJump == true)
        {
            textOfDoubleJumpOFF.gameObject.SetActive(false);
            textOfDoubleJump.gameObject.SetActive(true);

            //if in mode add points based on time

        }
        else if (pl.gotDoubleJump == false)
        {
            textOfDoubleJumpOFF.gameObject.SetActive(true);
            textOfDoubleJump.gameObject.SetActive(false);
        }

        if(pl.playerFail == true)
        {
            finalFrame.gameObject.SetActive(true);
            finalText.text = "You fell off the screen. Your score was : " + points + " points!";
            Time.timeScale = 0;
        }

        //POINTS = SCORE
        scoreText.text = "score: " + points;

        //adding points based on time - every 5s add some points
        pointTime -= Time.deltaTime;
        if(pointTime <= 0)
        {
            //give points
            Points(2);
            //restart timer
            pointTime = pointTimeStart;
        }

        //every fifth jump will get player random amount of score
        randomJumpScore = Random.Range(1,5);
        if(anotherJumpCount >= 5)
        {
            Points(randomJumpScore);
            anotherJumpCount = anotherJumpCountBase;
        }




    }

    IEnumerator StartCountDown()
    {
        text.text = "3";
        yield return new WaitForSeconds(1f);
        text.text = "2";
        yield return new WaitForSeconds(1f);
        text.text = "1";
        yield return new WaitForSeconds(1f);
        text.text = "GO";
        yield return new WaitForSeconds(1f);
        startGame = true;
        textAsGameObject.gameObject.SetActive(false);
    }

    IEnumerator ChaosOn()
    {
        textAsGameObject.gameObject.SetActive(true);
        text.text = "Chaos!";
        yield return new WaitForSeconds(1);
        textAsGameObject.gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Points(int number)
    {
        //if chaos is ON the points are doubled
        if(fistStageOfChaos == true)
        {
            points += number * 2;
        }else
        {
            points += number;
        }
        
    }

    //countdown - then start the game

    //score count

    //count jumps and after 20 jumps enter chaos
    // announce Chaos

    //next prepare that with different jump count you will display Let There Be Chaos ... one by one

    //end game - show score and play again button


}
