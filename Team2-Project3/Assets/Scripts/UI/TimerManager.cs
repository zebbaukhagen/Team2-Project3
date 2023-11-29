using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime = 0.0f; // 2 minutes in seconds
    public float timerCurrentTime;
    [SerializeField] private TMP_Text timer;
    public TMP_Text failed;
    public TMP_Text completeTime;
    public TMP_Text bestCompletedTime;
    public float bestPersonalTime;
    private UILevelController levelController;
    private VelocityBasedMovement playerMovement;
    private WinScript win;
    private GameManager gameManager;
    int minutes;
    int seconds;
    int bestMinutes;
    int bestSeconds;





    void Start()
    {
        win = GameObject.Find("Unicycle").GetComponent<WinScript>();
        levelController = GameObject.Find("Canvas").GetComponent<UILevelController>();
        playerMovement = GameObject.Find("Unicycle").GetComponent<VelocityBasedMovement>();
        timerCurrentTime = countdownTime;
        
        UpdateTimerText();



    }

    void Update()
    {
        if (playerMovement.playerCanMove)
        {
            if (timerCurrentTime >= 0.0f)
            {
                timerCurrentTime += Time.deltaTime; // Countdown by the time passed since the last frame
                UpdateTimerText();

            }
            else if (playerMovement.playerCanMove == false && playerMovement.playerHasFallen == true)
            {

                failed.text = "Time: " + Mathf.Max(0, Mathf.Ceil(timerCurrentTime));


            }
        }
    }

    public void SetLoseTime()
    {
        if (playerMovement.playerHasFallen == true)
        {
            failed.text = Mathf.Floor(minutes).ToString("00") + ":" + Mathf.Floor(seconds).ToString("00");

        }
    }

    public void SetWinTime()
    {
        if (playerMovement.playerBeatLevel == true)
        {
            completeTime.text = Mathf.Floor(minutes).ToString("00") + ":" + Mathf.Floor(seconds).ToString("00");
        }
    }

    public void SetBestTime()
    {
        if (playerMovement.playerBeatLevel == true)
        {
            bestCompletedTime.text = Mathf.Floor(bestMinutes).ToString("00") + ":" + Mathf.Floor(bestSeconds).ToString("00");
        }
    }

    void UpdateTimerText()
    {
        minutes = Mathf.FloorToInt(timerCurrentTime / 60);
        seconds = Mathf.FloorToInt(timerCurrentTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateBestTimeText()
    {
        bestPersonalTime = gameManager.bestTime;
        bestMinutes = Mathf.FloorToInt(bestPersonalTime / 60);
        bestSeconds = Mathf.FloorToInt(bestPersonalTime % 60);
        bestCompletedTime.text = string.Format("{0:00}:{1:00}", bestMinutes, bestSeconds); ;
    }
}


