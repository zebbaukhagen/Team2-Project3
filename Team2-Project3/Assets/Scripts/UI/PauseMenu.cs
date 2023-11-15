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
    //[SerializeField] private GameObject settingsMenu;
    //[SerializeField] private TMP_Text levelPrompt;
    [SerializeField] private UILevelController uiController;
    private static bool togglePauseGame;
    [SerializeField] private static bool pauseGame;
    //[SerializeField] private AudioManager audio;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Escape))
    //    {
    //        PauseGame();
    //    }
    //}

    //public void PauseGame()
    //{
    //    togglePauseGame = !togglePauseGame;

    //    if (togglePauseGame == true)
    //    {
    //        pauseMenu.SetActive(true);
    //    }
    //    else
    //    {
    //        togglePauseGame = false;
    //        pauseMenu.SetActive(false);
    //        Time.timeScale = 1f;
    //    }
    //}
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenSettings()
    {
        //settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void BackToMainMenu()
    {
        togglePauseGame = false;
        uiController.LoadScene("MainMenu");
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

}
