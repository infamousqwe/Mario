using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera camera;
    private Rigidbody2D rigidbody2D;
    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); // Corrected variable name
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
         position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        rigidbody2D.MovePosition(position);
    }
}