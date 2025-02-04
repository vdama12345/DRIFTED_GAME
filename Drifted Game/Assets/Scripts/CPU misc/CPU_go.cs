using UnityEngine;
using System.Collections;

public class CPUCollectible : MonoBehaviour
{
    [SerializeField] private GameObject CPU;
    private string collectedByPlayer = "";
    private Vector3 originalPosition; // <-- New field to store the original position

    private void Awake()
    {
        // Store the original position of this CPU collectible
        originalPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2") ||
            other.CompareTag("Player 3") || other.CompareTag("Player 4"))
        {
            collectedByPlayer = other.tag;
            Debug.Log($"Collected: {gameObject.name} by {collectedByPlayer}");
            ScoreManager.Instance.AddScore(gameObject.name);

            // Get the player's controller and add this CPU to their inventory
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddCollectedCPU(this);
            }

            // Deactivate the CPU collectible
            CPU.SetActive(false);
        }
    }

    // This method reactivates the CPU at its original position.
    public void Reactivate()
    {
        transform.position = originalPosition;
        CPU.SetActive(true);
    }
}
