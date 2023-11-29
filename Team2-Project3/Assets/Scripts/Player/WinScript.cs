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

            gameManager.bestTime = timer.timerCurrentTime;
            

            if (gameManager.bestTime >= timer.timerCurrentTime)
            {
                gameManager.bestTime = timer.timerCurrentTime;
            }
            Debug.Log(timer.timerCurrentTime);
            Debug.Log(gameManager.bestTime);

            timer.UpdateBestTimeText();
            timer.SetWinTime();
            timer.SetBestTime();
        }
    }
}
