using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    public Vector3 doorOpenPosition;

    //private Vector3 originalPressPosition;
    //public float pressDepth = 0.1f;
    private Vector3 doorClosedPosition;
    private bool isPressed = false;

    void Start()
    {
        //originalPressPosition = transform.position;
        if (door != null)
        {
            doorClosedPosition = door.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SolidSlime") || other.CompareTag("objtopush") && !isPressed)
        {
            //transform.position = originalPressPosition - new Vector3(0, pressDepth, 0);
            isPressed = true;
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SolidSlime") || other.CompareTag("objtopush") && isPressed)
        {
            //transform.position = originalPressPosition;
            isPressed = false;
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (door != null)
        {
            door.transform.position = doorOpenPosition;
        }
    }

    void CloseDoor()
    {
        if (door != null)
        {
            door.transform.position = doorClosedPosition;
        }
    }
}
