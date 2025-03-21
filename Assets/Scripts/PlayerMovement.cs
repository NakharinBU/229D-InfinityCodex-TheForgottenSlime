using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static SlimeMorph;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;

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

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();


        Vector3 moveDirection = forward * moveZ + right * moveX;
        moveDirection.Normalize();


        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (SlimeMorph.currentState == SlimeState.Solid)
        {
            rb.drag = 5f;
            rb.AddForce(moveDirection * SlimeMorph.solidSpeed, ForceMode.Acceleration);
        }
        else if (SlimeMorph.currentState == SlimeState.Liquid)
        {
            rb.drag = 1f;
            rb.AddForce(moveDirection * SlimeMorph.liquidSpeed, ForceMode.Acceleration);
        }
        else if (SlimeMorph.currentState == SlimeState.Gas)
        {
            rb.drag = 1f;
            rb.AddForce(moveDirection * SlimeMorph.gasSpeed, ForceMode.Acceleration);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (SlimeMorph.currentState == SlimeState.Solid) 
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                isGrounded = false;
            }
        }

        if (SlimeMorph.currentState == SlimeState.Gas && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * SlimeMorph.gasFloatSpeed, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
