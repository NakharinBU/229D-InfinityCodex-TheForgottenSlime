using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject indicatorLight;
    private bool isPressed;

    private DoorController doorController;

    void Start()
    {
        doorController = FindObjectOfType<DoorController>();
        if (indicatorLight != null) indicatorLight.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            isPressed = true;
            if (indicatorLight != null) indicatorLight.SetActive(true);
            doorController.UpdatePressureState(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            isPressed = false;
            if (indicatorLight != null) indicatorLight.SetActive(false);
            doorController.UpdatePressureState(-1);
        }
    }
}
