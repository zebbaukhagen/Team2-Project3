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

            if (!PlayerPrefs.HasKey("LevelOneCompleted"))
            {
                gameManager.bestTime = timer.timerCurrentTime;
            }
            else
            {
                // Compare times and update best time if necessary for replays
                if (timer.timerCurrentTime < gameManager.bestTime)
                {
                    gameManager.bestTime = timer.timerCurrentTime;

                }
                PlayerPrefs.SetInt("LevelOneCompleted", 1);

                timer.SetWinTime();
                timer.SetBestTime();
            }
        }
    }
}
