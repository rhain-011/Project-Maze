/*
 Author: Rhainel Peralta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPPController : MonoBehaviour
{
    // camera
    public float mouseSensitivityX = 3f;
    public float mouseSensitivityY = 3f;
    public Transform firstPersonCameraTransform;

    float verticalLookRotation;

    // player movement
    public float walkSpeed = 2.5f;
    public float jumpForce = 220f;
    public float fallMultiplier = 2.5f;
    public LayerMask groundedMask;
    public bool grounded;

    Vector3 smoothMoveVelocity;
    private new Rigidbody rigidbody;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstPersonCameraTransform = Camera.main.transform;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {

        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -45, 45);
        firstPersonCameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float inputY = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        Vector3 moveDir = new Vector3(inputX, 0, inputY);
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rigidbody.AddForce(transform.up * jumpForce);
            }
        }
        if (!grounded)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f, groundedMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


        // get back mouse cursor
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        transform.Translate(targetMoveAmount);
    }
}
