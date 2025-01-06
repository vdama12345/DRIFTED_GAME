using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class StellaPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D body;
    [SerializeField] private float speed;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocityX = Input.GetAxis("StellaHorizontal") * speed;
    }
}
