using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float countdownTime = 5.0f; // 2 minutes in seconds
    private float currentTime;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private UILevelController levelController;
    [SerializeField] private bool playerHasTime = true;
    


    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime; // Countdown by the time passed since the last frame
            UpdateTimerText();
        }
        else if (currentTime <= 0.0f) 
        {
            timer.text = string.Format("0:00");
            playerHasTime = false;
           
        }
        if(playerHasTime == false)
        {
            levelController.ActivateLosePanel();
        }
    }

    

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void PlayerRanOutOfTime()
    {
        if (!playerHasTime) 
        {
            
        }
       
    }
}
