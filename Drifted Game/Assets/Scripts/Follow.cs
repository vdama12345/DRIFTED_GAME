using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float targetDistance;
    private float Dis;
    private bool facingRight = true;

    private void Update()
    {
        Dis = Vector2.Distance(transform.position, Target.transform.position);

        if (Dis >= targetDistance)
        {
            Vector3 targetPos = Target.transform.position;

            // Move towards the target
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);

            // Flip the sprite if needed
            if ((targetPos.x < transform.position.x && facingRight) ||
                (targetPos.x > transform.position.x && !facingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Flip the object by scaling the X axis negatively
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

