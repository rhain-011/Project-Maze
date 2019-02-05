/*
 Coded by: Rhainel Peralta
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform camTarget;
    public float clampAngle = 80.0f; // indicates how far player can look on each axis;
    public float inputSensitivity = 150.0f;
    public float distanceFromTarget = 2.0f;
    public float mouseX;
    public float mouseY;

    // added for controller support
    public float inputX;
    public float inputZ;
    public float finalInputX;
    public float finalInputZ;
    private float rotX = 0.0f; // yaw
    private float rotY = 0.0f; // pitch

    public float rotationSmoothTime = 1.2f;
    Vector3 rotationSmoothVel;
    Vector3 currentRotation;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        inputX = Input.GetAxis("RightStickHorizontal");
        inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(rotX, rotY), ref rotationSmoothVel, rotationSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = camTarget.position - transform.forward * distanceFromTarget;


    }
}
