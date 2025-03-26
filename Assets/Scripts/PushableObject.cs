using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushForce = 5f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SlimeMorph player = collision.gameObject.GetComponent<SlimeMorph>();
            rb.isKinematic = true;

            if (player != null && player.currentState == SlimeMorph.SlimeState.Solid)
            {
                rb.isKinematic = false;
                Vector3 pushDirection = collision.transform.forward;
                rb.AddForce(pushDirection * pushForce, ForceMode.Force);
            }
        }
    }
}
