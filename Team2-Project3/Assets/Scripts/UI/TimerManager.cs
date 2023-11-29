using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float countUpTime = 0.0f; // 2 minutes in seconds
    public float timerCurrentTime;
    [SerializeField] private TMP_Text timer;
    public TMP_Text failed;
    public TMP_Text completeTime;
    public TMP_Text bestCompletedTime;
    public float bestPersonalTimeLevelOne;
    public float bestPersonalTimeLevelTwo;
    public float bestPersonalTimeLevelThree;
    private UILevelController levelController;
    private VelocityBasedMovement playerMovement;
    private WinScript win;
    private GameManager gameManager;
    int minutes;
    int seconds;
    int bestMinutes1;
    int bestSeconds1;
    int bestMinutes2;
    int bestSeconds2;
    int bestMinutes3;
    int bestSeconds3;





    void Start()
    {
        win = GameObject.Find("Unicycle").GetComponent<WinScript>();
        levelController = GameObject.Find("Canvas").GetComponent<UILevelController>();
        playerMovement = GameObject.Find("Unicycle").GetComponent<VelocityBasedMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timerCurrentTime = countUpTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (gameManager.playerIsAbleToMove)
        {
            if (timerCurrentTime >= 0.0f)
            {
                timerCurrentTime += Time.deltaTime; // Countdown by the time passed since the last frame
                UpdateTimerText();

            }
            else if (gameManager.playerIsAbleToMove == false && gameManager.playerHasFallen == true)
            {

                failed.text = "Time: " + Mathf.Max(0, Mathf.Ceil(timerCurrentTime));


            }
        }
    }

    public void SetLoseTime()
    {
        if (gameManager.playerHasFallen == true)
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
            if(SceneManager.GetActiveScene().name == "Level_1")
            {
                bestCompletedTime.text = Mathf.Floor(bestMinutes1).ToString("00") + ":" + Mathf.Floor(bestSeconds1).ToString("00");
            }
            else if (SceneManager.GetActiveScene().name == "Level_2")
            {
                bestCompletedTime.text = Mathf.Floor(bestMinutes2).ToString("00") + ":" + Mathf.Floor(bestSeconds2).ToString("00");
            }
            if (SceneManager.GetActiveScene().name == "Level_3")
            {
                bestCompletedTime.text = Mathf.Floor(bestMinutes3).ToString("00") + ":" + Mathf.Floor(bestSeconds3).ToString("00");
            }
        }
    }

    void UpdateTimerText()
    {
        minutes = Mathf.FloorToInt(timerCurrentTime / 60);
        seconds = Mathf.FloorToInt(timerCurrentTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateBestTimeTextLevelOne()
    {
        
        bestMinutes1 = Mathf.FloorToInt(bestPersonalTimeLevelOne / 60);
        bestSeconds1 = Mathf.FloorToInt(bestPersonalTimeLevelOne % 60);
        bestCompletedTime.text = string.Format("{0:00}:{1:00}", bestMinutes1, bestSeconds1); 
    }

    public void UpdateBestTimeTextLevelTwo()
    {

        bestMinutes2 = Mathf.FloorToInt(bestPersonalTimeLevelTwo / 60);
        bestSeconds2 = Mathf.FloorToInt(bestPersonalTimeLevelTwo % 60);
        bestCompletedTime.text = string.Format("{0:00}:{1:00}", bestMinutes2, bestSeconds2); 
    }

    public void UpdateBestTimeTextLevelThree()
    {

        bestMinutes3 = Mathf.FloorToInt(bestPersonalTimeLevelThree / 60);
        bestSeconds3 = Mathf.FloorToInt(bestPersonalTimeLevelThree % 60);
        bestCompletedTime.text = string.Format("{0:00}:{1:00}", bestMinutes2, bestSeconds2); 
    }
}


