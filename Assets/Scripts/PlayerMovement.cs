using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;

    private float verticalInput = 0;
    private float horizontalInput = 0;

    private Rigidbody rb;
    public Transform cameraTransform;

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

        /*Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);*/
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

}
