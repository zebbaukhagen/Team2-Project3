using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject CreditsMenu;
    private static UIMainMenuController instance;

    public static UIMainMenuController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIMainMenuController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("UIMainMenuController");
                    instance = singletonObject.AddComponent<UIMainMenuController>();
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        instructionsPanel.SetActive(false);
       

        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        LoadScene("Level_1");
        
    }

    public void OpenInstructions()
    {
        instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }


    public void OpenCreditPanel()
    {
        CreditsMenu.SetActive(!CreditsMenu.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit");
    }
}
