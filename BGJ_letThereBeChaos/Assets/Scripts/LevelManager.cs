using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int jumpCount;
    public int keepScoreCount;

    public bool startGame = false;

    public GameObject chaosCollection;
    public GameObject textAsGameObject;
    public Text text;

    public GameObject textOfDoubleJump;
    public GameObject doubleJumpInd;

    public Player pl;

    public GameObject finalFrame;
    public Text finalText;


    private void Start()
    {
        keepScoreCount = 0;
        Time.timeScale = 1;
        StartCoroutine(StartCountDown());
    }

    private void Update()
    {
        


        if(!startGame == false)
        {
            //Time.timeScale = 1;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                jumpCount++;
            }

            if (jumpCount == 10)
            {
                StartCoroutine(ChaosOn());
                //enter the chaos
                // enable all spawners
                chaosCollection.gameObject.SetActive(true);
            }
        } else
        {
            //Time.timeScale = 0;
        }


        if(pl.gotDoubleJump == true)
        {
            doubleJumpInd.gameObject.SetActive(true);
            textOfDoubleJump.gameObject.SetActive(true);
        }else if (pl.gotDoubleJump == false)
        {
            doubleJumpInd.gameObject.SetActive(false);
            textOfDoubleJump.gameObject.SetActive(false);
        }

        if(pl.playerFail == true)
        {
            finalFrame.gameObject.SetActive(true);
            finalText.text = "You fell off the screen. Your score was : " + keepScoreCount + " points!";
            Time.timeScale = 0;
        }

        
    }

    IEnumerator StartCountDown()
    {
        //Time.timeScale = 0;
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
        //Time.timeScale = 1;
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
    //countdown - then start the game

    //score count

    //count jumps and after 20 jumps enter chaos
    // announce Chaos

    //next prepare that with different jump count you will display Let There Be Chaos ... one by one

    //end game - show score and play again button


}
