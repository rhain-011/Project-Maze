using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // player movement
    public float walkSpeed = 2.5f;
    public float jumpForce = 220f;
    public float fallMultiplier = 2.5f;
    public LayerMask groundedMask;
    public bool grounded;

    Vector3 smoothMoveVelocity;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
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
