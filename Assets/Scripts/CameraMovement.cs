using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;

    public static float sensitivity = 2f;

    public Transform orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        transform.localEulerAngles = new Vector3(rotationX, 90 + rotationY, 0);
    }

}
