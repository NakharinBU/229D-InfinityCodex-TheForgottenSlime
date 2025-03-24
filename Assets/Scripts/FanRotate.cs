using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    public float rotationSpeed = 200f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
