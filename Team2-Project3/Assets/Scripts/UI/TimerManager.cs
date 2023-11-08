using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float countdownTime = 0.0f; // 2 minutes in seconds
    private float currentTime;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private UILevelController levelController;
    [SerializeField] private bool playerHasTime = true;
    [SerializeField] private UnicycleController unicycle;



    void Start()
    {
        unicycle = GameObject.Find("Unicycle").GetComponent<UnicycleController>();
        
        currentTime = countdownTime;
        UpdateTimerText();
    }

    void Update()
    {
        if(unicycle.hasControl)
        {
            if (currentTime >= 0.0f)
            {
                currentTime += Time.deltaTime; // Countdown by the time passed since the last frame
                UpdateTimerText();
                playerHasTime = true;

            }
            else if (currentTime <= 0.0f)
            {
                timer.text = string.Format("0:00");
                playerHasTime = false;

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
