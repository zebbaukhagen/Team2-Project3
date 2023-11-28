using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime = 0.0f; // 2 minutes in seconds
    public float currentTime;
    [SerializeField] private TMP_Text timer;
    public TMP_Text failed;
    public TMP_Text completeTime;
    [SerializeField] private UILevelController levelController;
    [SerializeField] private VelocityBasedMovement playerMovement;
    int minutes;
    int seconds;
    public float bestTime;




    void Start()
    {
         

        playerMovement = GameObject.Find("Unicycle").GetComponent<VelocityBasedMovement>();
        currentTime = countdownTime;
        UpdateTimerText();
        
    }

    void Update()
    {
        if(playerMovement.playerCanMove)
        {
            if (currentTime >= 0.0f)
            {
                currentTime += Time.deltaTime; // Countdown by the time passed since the last frame
                UpdateTimerText();

            }
            else if (playerMovement.playerCanMove == false && playerMovement.playerHasFallen == true)
            {

                failed.text = "Time: " + Mathf.Max(0, Mathf.Ceil(currentTime));
                

            }
        }
    }

    public void SetLoseTime()
    {
        if(playerMovement.playerHasFallen == true)
        {
            failed.text =  Mathf.Floor(minutes).ToString("00") + ":" + Mathf.Floor(seconds).ToString("00");
            Debug.Log(minutes);
            Debug.Log(seconds);
        }
    }


    void UpdateTimerText()
    {
        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
