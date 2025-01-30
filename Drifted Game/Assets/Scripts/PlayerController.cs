using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float speed;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode action;

    private bool grounded;

    private Animator anim;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = 0;

        // Handle movement for player 1 (WASD or other keys)
        if (Input.GetKey(right) || Input.GetKey(left))
        {
            if (Input.GetKey(right))
                horizontalInput = 1; // Move right
            else if (Input.GetKey(left))
                horizontalInput = -1; // Move left

            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            // Flip the sprite based on the direction
            if (horizontalInput > 0.01f)
            {
                Vector3 currentScale = transform.localScale;
                if (currentScale.x < 0)
                {
                    currentScale.x = -currentScale.x;
                }
                transform.localScale = currentScale;
            }
            else if (horizontalInput < -0.01f)
            {
                Vector3 currentScale = transform.localScale;
                if (currentScale.x > 0)
                {
                    currentScale.x = -currentScale.x;
                }
                transform.localScale = currentScale;
            }
        }

        // Jump if the player presses the "up" key and is grounded
        if (Input.GetKey(up) && grounded)
        {
            Jump();
        }

        // Update animation states
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed); // Apply jump force
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
