using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //[SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private TMP_Text levelPrompt;
    [SerializeField] private UILevelController uiController;
    [SerializeField] private static bool togglePauseGame;
    [SerializeField] private static bool pauseGame;
    [SerializeField] private AudioManager audio;
    
    
    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;
            if(pauseGame == true)
            {
                BackToMainMenu();
            }
            
        }
    }

    public void PauseGame()
    {
        togglePauseGame = !togglePauseGame;

        if (togglePauseGame == true)
        {
            //audio.PauseAudio();
            pauseMenu.SetActive(true);
            //playerController.enabled = false;
            Time.timeScale = 0f;
          
            if (SceneManager.GetActiveScene().name == "LevelOne")
            {
                levelPrompt.text = "Level 1: Escape The Flames! ";
            }
            else if (SceneManager.GetActiveScene().name == "LevelTwo")
            {
                levelPrompt.text = "Level 2: Connect The Conduits! ";
            }
            if(SceneManager.GetActiveScene().name == "LevelThree")
            {
                levelPrompt.text = "Level 1: Escape The Roomba! ";
            }
        }
        else
        {
            //audio.PlayMusic();
            togglePauseGame = false;
            pauseMenu.SetActive(false);
           //playerController.enabled = true;
            Time.timeScale = 1f;
        }
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void BackToMainMenu()
    {
        togglePauseGame = false;
        uiController.LoadScene("MainMenu");
        pauseMenu.SetActive(false);
        //playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

}
