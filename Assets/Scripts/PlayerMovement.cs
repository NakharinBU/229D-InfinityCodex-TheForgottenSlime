using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;
    SlimeMorph slimeMorph;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        slimeMorph = GetComponent<SlimeMorph>();
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

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        float currentSpeed = 0f;
        if (slimeMorph.currentState == SlimeMorph.SlimeState.Solid)
        {
            currentSpeed = slimeMorph.solidSpeed;
            rb.drag = 5f;
        }
        else if (slimeMorph.currentState == SlimeMorph.SlimeState.Liquid)
        {
            currentSpeed = slimeMorph.liquidSpeed;
            rb.drag = 1f;
        }
        else if (slimeMorph.currentState == SlimeMorph.SlimeState.Gas)
        {
            currentSpeed = slimeMorph.gasSpeed;
            rb.drag = 1f;
        }

        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (slimeMorph.currentState == SlimeMorph.SlimeState.Solid)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                isGrounded = false;
            }
        }

        if (slimeMorph.currentState == SlimeMorph.SlimeState.Gas && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * slimeMorph.gasFloatSpeed, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        if (slimeMorph.currentState != SlimeMorph.SlimeState.Gas)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
