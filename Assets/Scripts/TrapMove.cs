using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : MonoBehaviour
{
    public float rotationSpeed = 300f;
    public float moveSpeed = 2f;

    public Vector3 startPos;
    public Vector3 finalPos;

    private bool movingForward = true;

    void Start()
    {
        transform.position = startPos;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        Vector3 targetPos = movingForward ? finalPos : startPos;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            movingForward = !movingForward; // สลับทิศทาง
        }
    }
}
