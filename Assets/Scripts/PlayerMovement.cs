using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    SlimeMorph slimeMorph;
    private float slimeVelocity;
    Animator anim;
    public bool isTouchingCeiling = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        slimeMorph = GetComponent<SlimeMorph>();
        anim = GetComponent<Animator>();
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
        anim.SetFloat("Speed", moveDirection.magnitude * currentSpeed);

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            isTouchingCeiling = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            isTouchingCeiling = false;
        }
    }

    private void FixedUpdate()
    {
        if (slimeMorph.currentState != SlimeMorph.SlimeState.Gas)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void UpdateMorphState(SlimeMorph.SlimeState newState)
    {
        if (newState == SlimeMorph.SlimeState.Solid && slimeMorph.previousState == SlimeMorph.SlimeState.Gas)
        {
            StartCoroutine(FallFaster());
        }
    }

    private IEnumerator FallFaster()
    {
        float fallDuration = 1.5f;
        float timer = 0f;

        while (timer < fallDuration)
        {
            rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
