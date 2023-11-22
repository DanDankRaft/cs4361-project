using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivity = 1;
    // Start is called before the first frame update
    public Camera pCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    float yRotation = 0;
    // Update is called once per frame
    void Update()
    {
        yRotation -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        yRotation = Mathf.Clamp(yRotation, -45, 45);
        float xRotation = Input.GetAxisRaw("Mouse X") * sensitivity;

        Vector3 newCameraAngles = pCamera.transform.localEulerAngles;
        newCameraAngles.x = yRotation;
        pCamera.transform.localEulerAngles = newCameraAngles;
        
        transform.Rotate(Vector3.up * xRotation);
    }
}
