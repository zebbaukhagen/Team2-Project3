using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime = 0.0f; // 2 minutes in seconds
    private float currentTime;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text failed;
    [SerializeField] private UILevelController levelController;
    [SerializeField] private VelocityBasedMovement playerMovement;



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
                Debug.Log(currentTime);

            }
        }
        
      
    }



    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
