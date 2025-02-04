using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    private GameObject currentPlayer;
    private List<GameObject> cpuList = new List<GameObject>();

    private bool loadingNextLevel = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPos = transform.position;

        // Store references to all CPUs
        for (int i = 1; i <= ScoreManager.Instance.totalCPUs; i++)
        {
            GameObject cpu = GameObject.Find("CPU " + i);
            if (cpu != null)
            {
                cpuList.Add(cpu);
            }
        }

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
        ScoreManager.Instance.ResetScore();
        resetCPUs();
        Debug.Log(ScoreManager.Instance.score);
    }

    private void resetCPUs()
    {
        foreach (var cpu in cpuList)
        {
            if (cpu != null)
            {
                cpu.SetActive(true);
            }
        }
    }

    public void CheckCPUCompletion()
    {
        if (ScoreManager.Instance.score >= ScoreManager.Instance.totalCPUs && !loadingNextLevel)
        {
            StartCoroutine(ShowFlickeringLoadingScreen());
        }
    }

    IEnumerator ShowFlickeringLoadingScreen()
    {
        loadingNextLevel = true;

        GameObject flickerCanvas = new GameObject("FlickerCanvas");
        Canvas canvas = flickerCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        RectTransform rectTransform = flickerCanvas.GetComponent<RectTransform>();

        List<GameObject> blackSquares = new List<GameObject>();

        // Create black squares on the screen
        for (int i = 0; i < 50; i++)
        {
            GameObject blackSquare = new GameObject("BlackSquare" + i);
            blackSquare.transform.SetParent(flickerCanvas.transform);
            RectTransform squareRect = blackSquare.AddComponent<RectTransform>();
            blackSquare.AddComponent<CanvasRenderer>();

            var image = blackSquare.AddComponent<UnityEngine.UI.Image>();
            image.color = Color.black;

            squareRect.sizeDelta = new Vector2(Random.Range(50, 150), Random.Range(50, 150));
            squareRect.anchoredPosition = new Vector2(
                Random.Range(-Screen.width / 2, Screen.width / 2),
                Random.Range(-Screen.height / 2, Screen.height / 2)
            );

            blackSquares.Add(blackSquare);
        }

        // Flicker effect
        for (int flicker = 0; flicker < 10; flicker++)
        {
            foreach (var square in blackSquares)
            {
                square.SetActive(Random.value > 0.5f);
            }
            yield return new WaitForSeconds(0.1f);
        }

        // Clean up flickerCanvas
        Destroy(flickerCanvas);

        // Load next scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
}

