using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelController : MonoBehaviour
{
    //[SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenuPanel;
    private static UILevelController instance;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    public bool isPaused = false;
    [SerializeField] private VelocityBasedMovement playerMovement;

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
        //settingsMenu.SetActive(false);
        pauseMenuPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1;
        playerMovement = GameObject.Find("Unicycle").GetComponent<VelocityBasedMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        OpenPauseMenu();
    }

    public void OpenSettings()
    {
        //settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused == true)
            {
                pauseMenuPanel.SetActive(true);
                playerMovement.playerCanMove = false;
                Time.timeScale = 0;


            }
            else
            {
                isPaused = false;
                pauseMenuPanel.SetActive(false);
                playerMovement.playerCanMove = true;
                Time.timeScale = 1;
            }
        }
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        playerMovement.playerCanMove = false;
        Time.timeScale = 1;
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

    

    public void GoToMainMenu()
    {
        LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            LoadScene("Level_2");
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            LoadScene("Level_3");
        }
    }

    public void ActivateWinPanel()
    {
        winPanel.SetActive(true);
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
