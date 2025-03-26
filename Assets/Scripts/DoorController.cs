using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
    public GameObject doorEnter;
    private int activatedPlates = 0;
    private bool isOpen = false;

    public void UpdatePressureState(int change)
    {
        activatedPlates += change;

        if (activatedPlates >= 2)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }

    void Update()
    {
        if (isOpen)
        {
            door1.SetActive(false);
            door2.SetActive(false);
            doorEnter.SetActive(true);
        }
    }
}
