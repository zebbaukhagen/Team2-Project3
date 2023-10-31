using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    private void Awake()
    {
        respawnPoint = GameObject.Find("Respawn").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            Debug.Log("Player ate shit");
            Respawn();
        }
    }

    private void Respawn()
    {
        gameObject.transform.position = respawnPoint.position;
    }
}
