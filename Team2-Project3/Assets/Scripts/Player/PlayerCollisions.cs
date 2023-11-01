using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private UILevelController levelController;
    [SerializeField] Transform respawnPoint;
    private void Awake()
    {
        respawnPoint = GameObject.Find("Respawn").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LevelOneComplete"))
        {
            Debug.Log("finished level 1");
            levelController.ActivateWinPanel();
            
        }
    }

    private void Respawn()
    {
        gameObject.transform.position = respawnPoint.position;
    }
}
