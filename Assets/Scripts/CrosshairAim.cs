using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairAim : MonoBehaviour
{
    public Camera camera;
    public LayerMask targetLayer;

    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            Debug.Log("Target Hit: " + hit.collider.name);
        }
    }
}
