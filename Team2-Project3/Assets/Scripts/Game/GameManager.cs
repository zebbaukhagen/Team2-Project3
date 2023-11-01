using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioManager audioManager;
    private List<bool> listOfLevelsCompleted = new List<bool>();
    private bool levelOneCompleted = false;
    private bool levelTwoCompleted = false;
    private bool levelThreeCompleted = false;
    //[SerializeField] private GameObject playerCollisions;
    

    public List<bool> ListOfLevelsCompleted
    {
        get => listOfLevelsCompleted;
        private set => listOfLevelsCompleted = value;
    }
    private void Awake()
    {
        Debug.Log("GameManager awakened.");
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("GameManager destroyed.");
        }
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        Debug.Log("audioManager instance is equal to " + audioManager);
        InitializeLevelCompletionList();
        
        
    }

    private void Update()
    {
        CheatCodes();
    }

    private void InitializeLevelCompletionList()
    {
        listOfLevelsCompleted.Add(levelOneCompleted);
        listOfLevelsCompleted.Add(levelTwoCompleted);
        listOfLevelsCompleted.Add(levelThreeCompleted);
    }

    public void NextLevel()
    {
        // goes to the next scene regardless of current scene, in a circle
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextSceneIndex);
        }
        else
        {
            LoadLevel(1);
        }
    }

    public void LoadLevel(int buildIndexOfSceneToLoad)
    {
        // loads the appropriate level and music
        audioManager.PlayLevelMusic(buildIndexOfSceneToLoad);
        SceneManager.LoadScene(buildIndexOfSceneToLoad);
    }

    public void MarkPuzzleComplete(int levelToMark)
    {
        // marks the corresponding level as completed for level select
        listOfLevelsCompleted[levelToMark] = true;
        Debug.Log("level one completed? " + listOfLevelsCompleted[levelToMark]);
    }

    private void CheatCodes()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadLevel(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadLevel(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadLevel(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadLevel(6);
        }
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    playerCollisions.GetComponentInChildren<PlayerCollisions>().SetLevelCompleted();
        //}
    }
}