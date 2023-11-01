using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelController : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenuPanel;
    private static UILevelController instance;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject losePanel;

    public static UILevelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UILevelController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("UILevelController");
                    instance = singletonObject.AddComponent<UILevelController>();
                }
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
        pauseMenuPanel.SetActive(false);
        losePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        OpenPauseMenu();
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
    
    private void OpenPauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.PauseGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OffloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void GoToMainMenu()
    {
        LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        LoadScene("LevelTwo");
        OffloadScene("LevelOne");
    }

    public void ActivateLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void restartLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            LoadScene("Level_1");
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            LoadScene("Level_2");
        }
        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            LoadScene("Level_3");
        }
    }
}
