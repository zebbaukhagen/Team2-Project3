using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private UILevelController levelController;
    private VelocityBasedMovement playerMovement;
    private Timer timer;
    private GameManager gameManager;


    void Start()
    {
        levelController = GameObject.Find("Canvas").GetComponent<UILevelController>();
        playerMovement = GameObject.Find("Unicycle").GetComponent<VelocityBasedMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the trigger involves a specific tag
        if (other.gameObject.tag == "LevelOneComplete")
        {
            gameManager.playerIsAbleToMove = false;
            levelController.ActivateWinPanel();
            playerMovement.playerBeatLevel = true;

            if (SceneManager.GetActiveScene().name == "Level_1")
            {
                if (!gameManager.playerHasPlayedLevelOne)
                {
                    gameManager.bestTime1 = timer.timerCurrentTime;
                    timer.bestPersonalTimeLevelOne = gameManager.bestTime1;
                }
                else if (gameManager.playerHasPlayedLevelOne)
                {
                    gameManager.bestTime2 = timer.timerCurrentTime;

                    if (gameManager.bestTime1 > gameManager.bestTime2)
                    {
                        gameManager.bestTime1 = gameManager.bestTime2;
                        timer.bestPersonalTimeLevelOne = gameManager.bestTime2;
                    }
                    else
                    {
                        timer.bestPersonalTimeLevelOne = gameManager.bestTime1;
                    }
                }
                gameManager.playerHasPlayedLevelOne = true;
                timer.UpdateBestTimeTextLevelOne();
                timer.SetWinTime();
                timer.SetBestTime();
            }
        }



        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            if (!gameManager.playerHasPlayedLevelTwo)
            {
                gameManager.bestTime3 = timer.timerCurrentTime;
                timer.bestPersonalTimeLevelTwo = gameManager.bestTime3;
            }
            else if (gameManager.playerHasPlayedLevelTwo)
            {
                gameManager.bestTime4 = timer.timerCurrentTime;

                if (gameManager.bestTime3 > gameManager.bestTime4)
                {
                    gameManager.bestTime3 = gameManager.bestTime4;
                    timer.bestPersonalTimeLevelTwo = gameManager.bestTime3;
                }
                else
                {
                    timer.bestPersonalTimeLevelTwo = gameManager.bestTime1;
                }
            }
            gameManager.playerHasPlayedLevelTwo = true;
            timer.UpdateBestTimeTextLevelTwo();
            timer.SetWinTime();
            timer.SetBestTime();
        }


        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            if (!gameManager.playerHasPlayedLevelThree)
            {
                gameManager.bestTime5 = timer.timerCurrentTime;
                timer.bestPersonalTimeLevelThree = gameManager.bestTime5;
            }
            else if (gameManager.playerHasPlayedLevelThree)
            {
                gameManager.bestTime6 = timer.timerCurrentTime;

                if (gameManager.bestTime5 > gameManager.bestTime6)
                {
                    gameManager.bestTime5 = gameManager.bestTime6;
                    timer.bestPersonalTimeLevelThree = gameManager.bestTime5;
                }
                else
                {
                    timer.bestPersonalTimeLevelThree = gameManager.bestTime1;
                }
            }
            gameManager.playerHasPlayedLevelThree = true;
            timer.UpdateBestTimeTextLevelThree();
            timer.SetWinTime();
            timer.SetBestTime();
        }
    }
}


        

            
    


