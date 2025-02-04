using UnityEngine;

public class SwitchMovingPlatform : MonoBehaviour
{
    private MovePlatformUNactivated movingPlatform;

    private void Start()
    {
        // Ensure this script is on the same GameObject as MovingPlatform or properly assigned
        movingPlatform = GetComponent<MovePlatformUNactivated>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collision with the "Projectile" tag
        if (collision.gameObject.CompareTag("Projectile") && movingPlatform != null)
        {
            movingPlatform.TriggerPlatformMovement();
        }
    }
}
