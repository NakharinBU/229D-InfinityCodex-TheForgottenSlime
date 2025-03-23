using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlimeMorph;

public class FanController : MonoBehaviour
{
    public float windForce = 100f;
    public float magnusCoefficient = 2f;

    private void OnTriggerStay(Collider other)
    {
        SlimeMorph slime = other.GetComponent<SlimeMorph>();

        if (slime != null && slime.currentState == SlimeMorph.SlimeState.Gas)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 windDirection = transform.forward;
                Vector3 velocity = rb.velocity;
                Vector3 angularVelocity = rb.angularVelocity;

                rb.AddForce(windDirection * windForce, ForceMode.Acceleration);

                Vector3 magnusForce = magnusCoefficient * Vector3.Cross(velocity, angularVelocity);
                rb.AddForce(magnusForce, ForceMode.Acceleration);

                slime.isBeingBlown = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        SlimeMorph slime = other.GetComponent<SlimeMorph>();

        if (slime != null)
        {
            slime.isBeingBlown = false;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
