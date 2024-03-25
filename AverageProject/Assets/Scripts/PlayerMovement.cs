using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Camera camera;
    private Rigidbody2D rigidbody2D;
    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    private float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    private float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        inputAxis = Input.GetAxis("Horizontal");
        HorizontalMovement();
        CheckGrounded();
        if (grounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyGravity();
    }

    private void HorizontalMovement()
    {
        velocity.x = inputAxis * moveSpeed;
    }

    private void CheckGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1f; // Adjust this distance as necessary
        grounded = Physics2D.Raycast(position, direction, distance, ~LayerMask.GetMask("Player")).collider != null;
    }

    private void Jump()
    {
        velocity.y = jumpForce;
        grounded = false;
        jumping = true;
    }

    private void ApplyMovement()
    {
        Vector2 position = rigidbody2D.position + velocity * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(position);
    }

    private void ApplyGravity()
    {
        if (!grounded)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = 0;
        }
    }
}