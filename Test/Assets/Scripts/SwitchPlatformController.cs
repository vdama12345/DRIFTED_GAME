using UnityEngine;

public class SwitchPlatformController : MonoBehaviour
{
    public GameObject switchObject;
    public LayerMask projectileLayer;
    public GameObject platform;
    public Vector2 targetPosition;
    public float moveSpeed = 5f;

    private bool isActivated = false;
    private Vector2 initialPosition;

    void Start()
    {
        if (platform != null && platform.name == "PlatformMove")
        {
            initialPosition = platform.transform.position;
            Debug.Log("Platform initial position set.");
        }
        else
        {
            Debug.LogError("Platform not assigned or named incorrectly.");
        }
    }

    void Update()
    {
        if (isActivated && platform != null && platform.name == "PlatformMove")
        {
            MovePlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & projectileLayer) != 0)
        {
            Debug.Log("Projectile hit the switch!");
            isActivated = true;
            Destroy(collision.gameObject);
        }
    }

    private void MovePlatform()
    {
        if (platform != null && platform.name == "PlatformMove")
        {
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("Platform is moving.");

            if ((Vector2)platform.transform.position == targetPosition)
            {
                Debug.Log("Platform reached the target position!");
                isActivated = false;
            }
        }
    }

    public void ResetPlatform()
    {
        if (platform != null && platform.name == "PlatformMove")
        {
            platform.transform.position = initialPosition;
            isActivated = false;
            Debug.Log("Platform reset to initial position.");
        }
    }
}