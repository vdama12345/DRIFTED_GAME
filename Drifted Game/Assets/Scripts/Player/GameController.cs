using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    private GameObject currentPlayer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPos = transform.position;

        for (int i = 1; i <= 4; i++)
        {
            GameObject player = GameObject.FindWithTag("Player " + i);
            if (player != null && player == gameObject) // Ensure it's this instance
            {
                currentPlayer = player;
                Debug.Log(currentPlayer);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death Plane"))
        {
            Die();
            if (currentPlayer.CompareTag("Player 2"))
            {
                PlayerController.superJumpUsed = false;
            }
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
        ScoreManager.Instance.ResetScore();  // Reset score using the instance
        resetCPUs();
    }

    private void resetCPUs()
    {
        for (int i = 1; i <= ScoreManager.Instance.totalCPUs; i++)
        {
            GameObject currentCPU = GameObject.FindWithTag("CPU " + i);
            if (currentCPU != null)
            {
                currentCPU.SetActive(true);
            }
        }
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
}
