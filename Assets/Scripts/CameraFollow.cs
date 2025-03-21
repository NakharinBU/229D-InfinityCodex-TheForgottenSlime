using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float mouseSensitivity = 2f;
    public float defaultDistance = 5f;
    public Vector2 pitchMinMax = new Vector2(-40, 80);
    public LayerMask obstacleMask;

    private float cameraYaw = 0f;
    private float cameraPitch = 0f;
    private float currentDistance;

    void Start()
    {
        currentDistance = defaultDistance;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraYaw += mouseX;
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, pitchMinMax.x, pitchMinMax.y);

        transform.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);

        RaycastHit hit;
        Vector3 desiredPosition = target.position - transform.forward * defaultDistance;

        if (Physics.Raycast(target.position, -transform.forward, out hit, defaultDistance, obstacleMask))
        {
            currentDistance = hit.distance * 0.9f;
        }
        else
        {
            currentDistance = defaultDistance;
        }

        transform.position = target.position - transform.forward * currentDistance;
    }
}
