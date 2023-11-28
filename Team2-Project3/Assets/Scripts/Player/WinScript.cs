using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private UILevelController levelController;
    private VelocityBasedMovement playerMovement;
    private Timer timer;
    

    void Start()
    {
        levelController = GameObject.Find("Canvas").GetComponent<UILevelController>();
        playerMovement = GameObject.Find("Unicycle").GetComponent<VelocityBasedMovement>();
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the trigger involves a specific tag
        if (other.gameObject.tag == "LevelOneComplete")
        {
            playerMovement.playerCanMove = false;
            levelController.ActivateWinPanel();
        }
    }
}
