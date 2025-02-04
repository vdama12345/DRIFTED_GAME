using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class CPUCollectible : MonoBehaviour
{
    [SerializeField] private GameObject CPU; 
    private string collectedByPlayer = ""; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2") || other.CompareTag("Player 3") || other.CompareTag("Player 4")) 
        {
            collectedByPlayer = other.tag; 
            Debug.Log($"Collected: {gameObject.name} by {collectedByPlayer}"); 
            ScoreManager.Instance.AddScore(gameObject.name); 

            CPU.SetActive(false); 
        }
    }

    


}