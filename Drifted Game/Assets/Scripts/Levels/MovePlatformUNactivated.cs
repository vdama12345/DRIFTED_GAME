using UnityEngine;

public class MovePlatformUNactivated : MonoBehaviour
{
    public Transform posA, posB; // Target positions for movement
    public float speed = 2f; // Speed of movement

    private Vector3 targetPos; // Current target position
    private bool isMoving = false; // Tracks whether the platform is moving

    private void Start()
    {
        // Set the initial target position to posB
        targetPos = posB.position;
    }

    public void TriggerPlatformMovement()
    {
        // Toggle the target position between posA and posB
        targetPos = (targetPos == posA.position) ? posB.position : posA.position;
        isMoving = true; // Start moving the platform
    }

    private void Update()
    {
        // Move the platform if it's in motion
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            // Check if the platform has reached the target position
            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                isMoving = false; // Stop the platform
            }
        }
    }
}
