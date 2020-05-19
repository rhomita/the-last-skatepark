using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float xMouseSensivity = 90f;
    private float yMouseSensivity = 120f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private Quaternion newRotation;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        yRotation += Input.GetAxis("Mouse X") * xMouseSensivity * Time.deltaTime;
        xRotation -= Input.GetAxis("Mouse Y") * yMouseSensivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        newRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void FixedUpdate()
    {
        transform.localRotation = newRotation;
    }
}
