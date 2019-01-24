using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // mouse sensitivity on X and Y
    public float mouseSensitivityX = 3.0f;
    public float mouseSensitivityY = 3.0f;
    public Transform firstPersonCameraTransform;

    float verticalLookRotation;

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
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -45, 45);
        firstPersonCameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
