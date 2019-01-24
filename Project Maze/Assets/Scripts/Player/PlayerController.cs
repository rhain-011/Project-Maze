/*
 Author: Rhainel Peralta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // player movement
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] bool grounded;
    public LayerMask groundedMask;
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 3.0f;
    public float jumpForce = 220f;

    Vector3 targetMoveAmount;
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
        float inputX = Input.GetAxisRaw("Horizontal") * walkSpeed * Time.deltaTime;
        float inputY = Input.GetAxisRaw("Vertical") * walkSpeed * Time.deltaTime;

        Vector3 moveDir = new Vector3(inputX, 0, inputY);

        // run
        if (Input.GetKey("left shift"))
        {
            targetMoveAmount = moveDir * sprintSpeed;
        }
        else
        {
            targetMoveAmount = moveDir * walkSpeed;
        }

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

        transform.Translate(targetMoveAmount);
    }
}
