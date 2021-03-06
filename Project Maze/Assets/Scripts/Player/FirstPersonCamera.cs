﻿/*
 Coded by: Rhainel Peralta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // mouse sensitivity on X and Y
    public float mouseSensitivityX = 3.0f;
    public float mouseSensitivityY = 3.0f;
    public Transform firstPersonCameraTransform;
    public float vertLookClamp = 45.0f;
    private float verticalLookRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstPersonCameraTransform = firstPersonCameraTransform.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Look rotation:
        transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") + Input.GetAxis("RightStickHorizontal")) * mouseSensitivityX);
        verticalLookRotation += (Input.GetAxis("Mouse Y") + Input.GetAxis("RightStickHorizontal")) * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -vertLookClamp, vertLookClamp); // limit look rotation in the y axis
        firstPersonCameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;



        // get back mouse cursor
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
