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
    [SerializeField] GameManager gameManager;

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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if(gameManager.playerIsAbleToMove)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;

                if (isPaused == true)
                {
                    pauseMenuPanel.SetActive(true);
                    gameManager.playerIsAbleToMove = false;
                }
                else
                {
                    isPaused = false;
                    pauseMenuPanel.SetActive(false);
                    gameManager.playerIsAbleToMove = true;
                }
            }
        }
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        gameManager.playerIsAbleToMove = true;
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
            gameManager.playerIsAbleToMove = true;
            gameManager.playerHasFallen = false;
            LoadScene("Level_2");
           
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            gameManager.playerIsAbleToMove = true;
            gameManager.playerHasFallen = false;
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
            gameManager.playerIsAbleToMove = true;
            gameManager.playerHasFallen = false;
            LoadScene("Level_1");
          
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            gameManager.playerIsAbleToMove = true;
            gameManager.playerHasFallen = false;
            LoadScene("Level_2");
          
        }
        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            gameManager.playerIsAbleToMove = true;
            gameManager.playerHasFallen = false;
            LoadScene("Level_3");
           
        }
    }
}
