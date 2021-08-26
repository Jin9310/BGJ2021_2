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
    public GameObject chaosTwoCollection;
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
    //first stage variables
    [SerializeField] private int doubleJumpCount;
    [SerializeField] private bool counted = false;
    private bool startSecondTimer = false;
    private float secondStageTimer = 30f;

    //second stage variables
    public bool secondStageOfChaos = false;

    //third stage variables
    public bool thirdStageOfChaos = false;
    


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
                jumpCount++;
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
            finalText.text = "You fell off the screen. Your score was : " + Mathf.FloorToInt(points) + " points!";
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
            Time.timeScale = 0;
        }

        //POINTS = SCORE
        scoreText.text = "score: " + Mathf.FloorToInt(points);

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

        if(fistStageOfChaos == true)
        {
            if(pl.gotDoubleJump == true && counted == false)
            {
                doubleJumpCount += 1;
                counted = true;
            }else if(pl.gotDoubleJump == false)
            {
                counted = false;
            }
        }


        //STAGE 2
        if (doubleJumpCount == 3)
        {
            //turns off the counting as it is not needed anymore
            fistStageOfChaos = false;
            secondStageOfChaos = true;
            //anounces the stage 2 of chaos
            StartCoroutine(ChaosStageTwoOn());
            //disables all the additional spawners from previous stage
            chaosCollection.gameObject.SetActive(false);
            doubleJumpCount++;
            startSecondTimer = true;

            chaosTwoCollection.gameObject.SetActive(true);

        }

        //second stage timer
        if(startSecondTimer == true)
        {
            secondStageTimer -= Time.deltaTime;
            if(secondStageTimer <= 0)
            {
                //turn off second stage
                secondStageOfChaos = false;
                //start STAGE 3
                thirdStageOfChaos = true;
                StartCoroutine(ChaosStageThreeOn());
                //enable all platforms from stage one again
                //also everything from stage 2 will remain ON
                chaosCollection.gameObject.SetActive(true);
                startSecondTimer = false;
            }
        }

        //STAGE 3
        //start timer of third stage
        //after the timer runs out get to the finale .. not sure what that is


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
        //1st stage of chaos
        textAsGameObject.gameObject.SetActive(true);
        text.text = "Let!";
        yield return new WaitForSeconds(1);
        textAsGameObject.gameObject.SetActive(false);
    }

    IEnumerator ChaosStageTwoOn()
    {
        //1st stage of chaos
        textAsGameObject.gameObject.SetActive(true);
        text.text = "There Be";
        yield return new WaitForSeconds(1);
        textAsGameObject.gameObject.SetActive(false);
    }

    IEnumerator ChaosStageThreeOn()
    {
        //1st stage of chaos
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
            points += number * 1.5f;
        }else
        {
            points += number;
        }

        if (secondStageOfChaos == true)
        {
            points += number * 2;
        }
        else
        {
            points += number;
        }

        if (thirdStageOfChaos == true)
        {
            points += number * 3;
        }
        else
        {
            points += number;
        }

    }

   /* DEV PLAN
    * 
    * 3 stages of Chaos
    *   1st stage adds platforms and multiplies score by 2
    *       - count double jumps powerup pickups - after 3 pickups move to stage 2
    *   
    *   2nd stage
    *   - turns off platforms and dash?
    *   - dash is now collectible?
    *   - randomly spawns new pick up effects
    *       
    *   - and 
    *   
    *   3rd stage after another 20 secs
    *   - all together
    *   - will last only 20s
    *   
    *   
    *   ending shows up
    
    
    */


}
