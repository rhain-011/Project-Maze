using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    [SerializeField] bool grounded;
    public LayerMask groundedMask;

    private PlayerStatManager playerStat;
    private Rigidbody rb;
    private float walkSpeed;
    private float sprintSpeed;
    private float jumpForce;
    private float fallMultiplier;

    private Vector3 targetMoveAmount;
    //public Camera tdCamera; // main camera reference

    // Start is called before the first frame update
    void Awake()
    {
        playerStat = GetComponent<PlayerStatManager>();
        rb = GetComponent<Rigidbody>();

        walkSpeed = playerStat.p_DefaultWalkSpeed;
        sprintSpeed = playerStat.p_DefaultRunSpeed;
        jumpForce = playerStat.p_DefaultJumForce;
        fallMultiplier = playerStat.p_DefaultFallMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.Translate(targetMoveAmount);
    }
}
