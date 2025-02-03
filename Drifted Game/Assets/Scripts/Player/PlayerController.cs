using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpingPower = 10f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode action;
    [SerializeField] private Text canvasText; // Text to display messages
    private bool isWallRunning = false; // This will keep track of whether the player is currently wall running


    private GameObject currentPlayer;
    private bool grounded;
    [HideInInspector] public static bool superJumpUsed = false;
    private bool speedBoostActive = false;
    private float speedBoostTimer = 0f;
    private float speedBoostCooldown = 40f;
    private float speedBoostDuration = 10f;
    private Animator anim;
    private Rigidbody2D body;

    private static readonly string OpeningStreetScene = "OpeningStreet";
    private Camera mainCamera;
    private List<GameObject> players;

    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        players = GetActivePlayers();

        SetCurrentPlayer();
    }

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (dialogueUI != null && dialogueUI.IsOpen) return;

        HandleNormalSceneMovement(currentScene);
        UpdateCameraPosition(currentScene);
    }

    private void HandleNormalSceneMovement(string currentScene)
    {
        HandleMovementInput(currentScene);
    }

    private void SetCurrentPlayer()
    {
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

    private void HandleMovementInput(string currentScene)
    {
        float horizontalInput = 0;

        if (Input.GetKey(right))
            horizontalInput = 1;
        else if (Input.GetKey(left))
            horizontalInput = -1;

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        Vector3 currentScale = transform.localScale;

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);

        if (Input.GetKeyDown(up) && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
            anim.SetTrigger("Jump");
            grounded = false;
        }

        if (Input.GetKeyDown(action))
        {
            Debug.Log(currentPlayer);
            if (currentPlayer != null)
            {
                if (currentPlayer.CompareTag("Player 1"))
                {
                    Debug.Log(currentPlayer);
                    if (currentScene == "OpeningStreet")
                    {
                        Interactable?.Interact(this);
                    }
                    else
                    {
                        ShootProjectile();
                    }
                }
                else if (currentPlayer.CompareTag("Player 2") && currentScene != OpeningStreetScene)
                {
                    Debug.Log(currentPlayer);
                    HandleSuperJump(currentPlayer);
                }
                else if (currentPlayer.CompareTag("Player 3") && currentScene != OpeningStreetScene)
                {
                    Debug.Log(currentPlayer);
                    HandleVerticalWallRun(currentPlayer);
                }
                else if (currentPlayer.CompareTag("Player 4") && currentScene != OpeningStreetScene)
                {
                    Debug.Log(currentPlayer);
                    HandleSpeedBoost(currentPlayer);
                }
            }
            Debug.Log(currentPlayer);
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);

        if (speedBoostActive)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0f)
            {
                speedBoostActive = false;
                speed = 5f; // Reset speed
            }
        }
    }

    private void HandleSuperJump(GameObject player)
    {
        if (!superJumpUsed)
        {
            Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
            playerBody.linearVelocity = new Vector2(playerBody.linearVelocity.x, jumpingPower * 1.8f); // Higher jump
            superJumpUsed = true;
        }
        else
        {
            DisplayCanvasMessage("Super jump can only be used once per level.");
        }
    }

    private void HandleVerticalWallRun(GameObject player)
    {
        // Ensure that only Player 3 can trigger the action
        if (!player.CompareTag("Player 3")) return;

        Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();

        // Toggle wall run on key press
        if (Input.GetKeyDown(action))
        {
            // If player is already wall running, stop
            if (isWallRunning)
            {
                StopWallRun(playerBody, player);
            }
            else
            {
                StartWallRun(playerBody, player);
            }
        }
    }

    private void StartWallRun(Rigidbody2D playerBody, GameObject player)
    {
        // Raycast to check if a wall is nearby
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.right * transform.localScale.x, 0.5f);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // Start wall run
            playerBody.gravityScale = 0f;
            playerBody.linearVelocity = new Vector2(0f, speed); // Move upwards at a fixed speed
            player.transform.rotation = Quaternion.Euler(0f, 0f, -90f * Mathf.Sign(transform.localScale.x)); // Align player to the wall

            isWallRunning = true; // Set wall running state to true
        }
        else
        {
            // If no wall is detected, display a message or prevent wall run
            DisplayCanvasMessage("No wall detected for wall running.");
        }
    }

    private void StopWallRun(Rigidbody2D playerBody, GameObject player)
    {
        // Reset the player's state when wall running stops
        playerBody.gravityScale = 1f; // Reset gravity to default
        player.transform.rotation = Quaternion.identity; // Reset player rotation to normal
        isWallRunning = false; // Set wall running state to false
    }

    private void HandleSpeedBoost(GameObject player)
    {
        if (speedBoostActive)
        {
            DisplayCanvasMessage("Speed boost is on cooldown.");
            return;
        }

        speedBoostActive = true;
        speed = 10f;
        speedBoostTimer = speedBoostDuration;
        Invoke(nameof(ResetSpeedBoost), speedBoostDuration);
        Invoke(nameof(ClearCanvasMessage), speedBoostCooldown);
    }

    private void ResetSpeedBoost()
    {
        speed = 5f; // Reset speed
        speedBoostActive = false;
    }

    private void DisplayCanvasMessage(string message)
    {
        if (canvasText != null)
        {
            canvasText.text = message;
        }
    }

    private void ClearCanvasMessage()
    {
        if (canvasText != null)
        {
            canvasText.text = "";
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
            if (projectileBody != null)
            {
                float direction = transform.localScale.x > 0 ? 1 : -1;
                projectileBody.linearVelocity = new Vector2(direction * projectileSpeed, 0);
            }
        }
        else
        {
            Debug.LogWarning("Projectile prefab or spawn point is not set!");
        }
    }

    private List<GameObject> GetActivePlayers()
    {
        List<GameObject> activePlayers = new List<GameObject>();
        for (int i = 1; i <= 4; i++)
        {
            GameObject player = GameObject.FindWithTag("Player " + i);
            if (player != null)
            {
                activePlayers.Add(player);
            }
        }
        return activePlayers;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void UpdateCameraPosition(string currentScene)
    {
        if (currentScene != OpeningStreetScene && mainCamera != null)
        {
            Vector3 averagePosition = Vector3.zero;
            int activePlayerCount = 0;

            float maxDistance = 0f;
            GameObject frontMostPlayer = null;

            // Calculate average position and find the front-most player
            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    averagePosition += player.transform.position;
                    activePlayerCount++;

                    // Find the player that is furthest to the right (the front-most)
                    if (frontMostPlayer == null || player.transform.position.x > frontMostPlayer.transform.position.x)
                    {
                        frontMostPlayer = player;
                    }
                }
            }

            if (activePlayerCount > 0)
            {
                averagePosition /= activePlayerCount;

                // Calculate the spread of players to determine zoom
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count; j++)
                    {
                        if (players[i] != null && players[j] != null)
                        {
                            float distance = Vector3.Distance(players[i].transform.position, players[j].transform.position);
                            maxDistance = Mathf.Max(maxDistance, distance);
                        }
                    }
                }

                // Adjust camera position
                averagePosition.z = -10f;

                // If there's a front-most player, offset the camera slightly towards them
                if (frontMostPlayer != null)
                {
                    Vector3 frontOffset = new Vector3(1.5f, 0f, 0f);  // Offset towards the front-most player
                    averagePosition = Vector3.Lerp(averagePosition, frontMostPlayer.transform.position + frontOffset, 0.25f);
                }

                // Smoothly move the camera towards the average position
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, averagePosition, Time.deltaTime * 5f);

                // Adjust camera zoom based on player spread
                float targetZoom = Mathf.Clamp(5f + maxDistance * 0.25f, 6f, 30f); // Clamp zoom between 10 and 30
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * 2f);
            }
        }
        else
        {
            ConstrainPlayerWithinCameraBounds();
        }
    }

    private void ConstrainPlayerWithinCameraBounds()
    {
        if (mainCamera != null)
        {
            // Calculate camera boundary limits based on camera's position and size
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            // Clamp player position to within the camera bounds
            Vector3 constrainedPosition = transform.position;

            // Horizontal (X) bounds
            float minX = mainCamera.transform.position.x - cameraWidth / 2;
            float maxX = mainCamera.transform.position.x + cameraWidth / 2;

            // Vertical (Y) bounds
            float minY = mainCamera.transform.position.y - cameraHeight / 2;
            float maxY = mainCamera.transform.position.y + cameraHeight / 2;

            constrainedPosition.x = Mathf.Clamp(constrainedPosition.x, minX, maxX);
            constrainedPosition.y = Mathf.Clamp(constrainedPosition.y, minY, maxY);

            transform.position = constrainedPosition;
        }
    }
}
