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

        if (Input.GetKey(right) || Input.GetKey(left))
        {
            horizontalInput = Input.GetAxis("Horizontal");
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

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

        if (Input.GetKey(up) && grounded)
        {
            Jump();
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
