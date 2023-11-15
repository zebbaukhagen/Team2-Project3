using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] private UILevelController levelController;
    [SerializeField] private VelocityBasedMovement playerMovement;

    void Start()
    {
        //unicycleController = GameObject.Find("Unicycle").GetComponent<UnicycleController>();
        levelController = GameObject.Find("Canvas").GetComponent<UILevelController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("LevelOneComplete"))
        {
            Debug.Log("you finished!");
            levelController.ActivateWinPanel();
        }
    }
}
