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
    public GameObject chaosThreeCollection;
    public GameObject basicSpawner;
    public GameObject finalBoundaries;

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
    public Text randomChaosQuote;

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
    private bool startThirdTimer = false;
    private float thirdStageTimer = 20f;


    //final stage = credits
    public bool finalStage = false;
    public GameObject finalScreenFrame;
    public Text textToBeGenerated;
    public Text finalScoreToBeDisplayed;

    private int randomQuote;

    int timeNumber = 1;
    float creditsTimer;
    float creditTimerStart = 5f;




    private void Start()
    {
        creditsTimer = creditTimerStart;
        randomQuote = Random.Range(0, 5);
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
            /*
            finalFrame.gameObject.SetActive(true);
            finalText.text = "You fell off the screen. Your score was : " + Mathf.FloorToInt(points) + " points!";
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }*/
            FinalResults();
            Time.timeScale = 0;
        }

        //POINTS = SCORE
        scoreText.text = "score: " + Mathf.FloorToInt(points);

        if (finalStage != true)
        {
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
        
            randomJumpScore = Random.Range(1, 5);
            if (anotherJumpCount >= 5)
            {
                Points(randomJumpScore);
                anotherJumpCount = anotherJumpCountBase;
            }

            if (fistStageOfChaos == true)
            {
                if (pl.gotDoubleJump == true && counted == false)
                {
                    doubleJumpCount += 1;
                    counted = true;
                }
                else if (pl.gotDoubleJump == false)
                {
                    counted = false;
                }
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
        if(thirdStageOfChaos == true)
        {
            chaosThreeCollection.gameObject.SetActive(true);
            startThirdTimer = true;
        }

        if(startThirdTimer == true)
        {
            thirdStageTimer -= Time.deltaTime;
            if(thirdStageTimer <= 0)
            {
                finalStage = true;
                //turn off all spawners
                chaosCollection.gameObject.SetActive(false);
                chaosTwoCollection.gameObject.SetActive(false);
                chaosThreeCollection.gameObject.SetActive(false);
                basicSpawner.gameObject.SetActive(false);

                //turn on side boundaries so player can' t fall off
                finalBoundaries.gameObject.SetActive(true);

             }
        }

        if(finalStage == true)
        {
            finalScreenFrame.gameObject.SetActive(true);
            finalScoreToBeDisplayed.text = "Final score: " + Mathf.FloorToInt(points);

            //some timer
            creditsTimer -= Time.deltaTime;
            if(creditsTimer <= 0)
            {
                if(timeNumber < 6)
                {
                    timeNumber++;
                }
                creditsTimer = creditTimerStart;
            }

            TypeCredits();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }

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

    private void FinalResults()
    {
        finalFrame.gameObject.SetActive(true);
        finalText.text = "You fell off the screen. Your score was : " + Mathf.FloorToInt(points) + " points!";

        switch (randomQuote)
        {
            case 0:
                randomChaosQuote.text = "chaos - complete disorder and confusion";
                break;
            case 1:
                randomChaosQuote.text = "chaos - the property of a complex system whose behaviour is so unpredictable as to appear random, owing to great sensitivity to small changes in conditions";
                break;
            case 2:
                randomChaosQuote.text = "chaos - the formless matter supposed to have existed before the creation of the universe";
                break;
            case 3:
                randomChaosQuote.text = " chaos - a state of utter confusion or disorder";
                break;
            case 4:
                randomChaosQuote.text = "chaos - a total lack of organization or order";
                break;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void TypeCredits()
    {
        switch (timeNumber)
        {
            case 1:
                textToBeGenerated.text = "The game was made as part of Brackeys Game Jam";
                break;
            case 2:
                textToBeGenerated.text = "Theme of the jam was : Let there be chaos";
                break;
            case 3:
                textToBeGenerated.text = "tools: UNITY, Photoshop, FL Studio, pen & paper";
                break;
            case 4:
                textToBeGenerated.text = "Special thanks to my friends Lukas and Petr that helped me with early feedback and testing and to my wife for huge support.";
                break;
            case 5:
                textToBeGenerated.text = "Thank you, dear player, for playing the game";
                break;
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
