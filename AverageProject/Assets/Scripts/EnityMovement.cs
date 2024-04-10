using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    public LayerMask groundLayer; // LayerMask to detect ground
    public LayerMask obstacleLayer; // LayerMask to detect obstacles

    public float checkDistance = 0.5f; // Distance to check for ground and obstacles

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        #if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
        #else
        enabled = true;
        #endif
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        // Move the entity
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        // Check for obstacles and flip direction if one is detected
        if (CheckForObstacles())
        {
            direction = -direction;
        }

        // Ensure that the entity doesn't fall through the ground
        if (IsGrounded())
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        // Flip the entity based on the movement direction
        if (direction.x > 0f)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x < 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private bool CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, checkDistance, obstacleLayer);
        return hit.collider != null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        return hit.collider != null;
    }
}