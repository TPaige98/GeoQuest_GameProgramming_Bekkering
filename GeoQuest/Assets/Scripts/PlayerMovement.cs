using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Variable for Character Movement
    float horizontalInput;
    public float walkSpeed;

    //Varibles for Character Jumping
    public float jumpHeight;
    int jumpsLeft;

    //Variables for Checking Ground
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(1.45f, 0.05f);
    public LayerMask groundLayer;

    //Variables for Gravity
    public float initialGravity = 2f;
    public float maxGravity = 20f;
    public float fallSpeedGravity = 2f;

    //Variables for Unity Objects
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        gravity();
        doubleJump();
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsLeft > 0)
        {
            if (context.performed && context.action.name == "Jump")
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                jumpsLeft--;
            }
            else if (context.canceled && context.action.name == "Jump")
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsLeft--;
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    private void doubleJump()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsLeft = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = initialGravity * fallSpeedGravity;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxGravity));
        }
        else
        {
            rb.gravityScale = initialGravity;
        }
    }
}
