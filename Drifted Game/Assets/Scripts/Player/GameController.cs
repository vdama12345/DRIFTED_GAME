using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    private GameObject currentPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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

    // Update is called once per frame
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
        ScoreManager.score = 0;

    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
}
