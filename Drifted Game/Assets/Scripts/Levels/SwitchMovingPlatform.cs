using System.Collections.Generic;
using UnityEngine;
public class SwitchMovingPlatform : MonoBehaviour
{
    public MovePlatformUNactivated platform;
    private bool movingToA = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (movingToA)
            {
                platform.MoveToB();
                movingToA = false;
            }
            else
            {
                platform.MoveToA();
                movingToA = true;
            }
        }
    }
}
