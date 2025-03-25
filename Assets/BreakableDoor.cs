using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    public float requiredForce = 10f;
    private Rigidbody rb;
    private bool isOpened = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isOpened) return;

        SlimeMorph slime = collision.gameObject.GetComponent<SlimeMorph>();
        if (slime != null && slime.currentState == SlimeMorph.SlimeState.Solid)
        {
            float impactForce = collision.relativeVelocity.magnitude;
            Debug.Log("Impact Force: " + impactForce);

            if (impactForce >= requiredForce)
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    IEnumerator OpenDoor()
    {
        isOpened = true;
        rb.isKinematic = false;
        rb.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
