using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static SlimeMorph;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;

    SlimeMorph SlimeMorph;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SlimeMorph = GetComponent<SlimeMorph>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float currentSpeed = (SlimeMorph.currentState == SlimeState.Solid) ? SlimeMorph.solidSpeed :
                             (SlimeMorph.currentState == SlimeState.Liquid) ? SlimeMorph.liquidSpeed :
                             SlimeMorph.gasSpeed;

        Vector3 movement = new Vector3(moveX, 0, moveZ) * currentSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        
        if (SlimeMorph.currentState == SlimeState.Liquid)
        {
            rb.drag = 5f;
        }
        else
        {
            rb.drag = 1f;
        }

        if (SlimeMorph.currentState == SlimeState.Gas && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * SlimeMorph.gasFloatSpeed, ForceMode.Acceleration);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            if (SlimeMorph.currentState == SlimeState.Solid)
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        if (SlimeMorph.currentState == SlimeState.Gas && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * SlimeMorph.gasFloatSpeed, ForceMode.Acceleration);
        }
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
