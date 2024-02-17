using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string playerTag; // Tag of the player GameObjects
    public int scoreValue = 5; // Score value when collected

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.IncrementScore(scoreValue);
                Destroy(gameObject); // Destroy the collectible
            }
        }
    }
}