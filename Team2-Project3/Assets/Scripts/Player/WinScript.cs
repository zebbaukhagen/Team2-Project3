using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            playerMovement.playerCanMove = false;
            levelController.ActivateWinPanel();
            playerMovement.playerBeatLevel = true;

            if (!gameManager.playerHasPlayedLevelOne)
            {
                gameManager.bestTime1 = timer.timerCurrentTime;
                timer.bestPersonalTime = gameManager.bestTime1;
            }
            else if (gameManager.playerHasPlayedLevelOne)
            {
                gameManager.bestTime2 = timer.timerCurrentTime;

                if (gameManager.bestTime1 > gameManager.bestTime2)
                {
                    gameManager.bestTime1 = gameManager.bestTime2;
                    timer.bestPersonalTime = gameManager.bestTime2;
                }
                else
                {
                    timer.bestPersonalTime = gameManager.bestTime1;
                }
            }
        }
        gameManager.playerHasPlayedLevelOne = true;
        timer.UpdateBestTimeText();
        timer.SetWinTime();
        timer.SetBestTime();
    }
}

