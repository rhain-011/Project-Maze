/*
 Coded by: Rhainel Peralta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // player movement
    [SerializeField] bool grounded;
    public LayerMask groundedMask;

    private float walkSpeed;
    private float sprintSpeed;
    private float jumpForce;
    private float fallMultiplier;

    Vector3 targetMoveAmount;
    private Rigidbody rb;
    private PlayerStatManager playerStat;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        playerStat = GetComponent<PlayerStatManager>();
        walkSpeed = playerStat.p_DefaultWalkSpeed;
        sprintSpeed = playerStat.p_DefaultRunSpeed;
        jumpForce = playerStat.p_DefaultJumForce;
        fallMultiplier = playerStat.p_DefaultFallMultiplier;
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

        // checks if player is on the ground
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, groundedMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        // accelerate fall after player jumps
        if (!grounded)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // move player
        transform.Translate(targetMoveAmount);
    }
}
