using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotate : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float moveSpeed = 2f;
    public float moveHeight = 1f;

    private Vector3 startPos;
    private bool goingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        float newY = transform.position.y + (goingUp ? moveSpeed : -moveSpeed) * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (Mathf.Abs(newY - startPos.y) >= moveHeight)
        {
            goingUp = !goingUp;
        }
    }
}
