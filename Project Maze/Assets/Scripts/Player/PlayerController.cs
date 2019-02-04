/*
 Coded by: Rhainel Peralta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] bool isRunning;
    [SerializeField] bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundedMask;
    
    private Vector3 moveDir = Vector3.zero;
    public Vector3 targetMoveAmount = Vector3.zero;

    private float walkSpeed;
    private float sprintSpeed;
    private float jumpForce;
    private float fallMultiplier;

    public float speedSmoothTime = 0.0f;
    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;

    private Rigidbody rb;
    private PlayerStatManager playerStat;
    private Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
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
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        float finalInputX = inputX * walkSpeed * Time.deltaTime;
        float finalInputY = inputY * walkSpeed * Time.deltaTime;

        moveDir = new Vector3(finalInputX, 0, finalInputY);
        
        // checks if player is on the ground
        Ray ray = new Ray(groundCheck.transform.position, -groundCheck.transform.up);
        //Debug.DrawRay(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.08f, groundedMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        // accelerate fall after player jumps
        if (!isGrounded)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // move player
        targetMoveAmount = moveDir;

        // run
        //if (Input.GetKey("left shift"))
        //{
        //    targetMoveAmount = moveDir * sprintSpeed;
        //}

        //transform.Translate(targetMoveAmount);

        Vector2 inputDir = new Vector2 (inputX, inputY).normalized;


        isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((isRunning) ? sprintSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        transform.Translate(targetMoveAmount * currentSpeed);

        float animSpeedPercent = ((isRunning) ? 1 : 0.5f) * inputDir.magnitude;
        anim.SetFloat("speedPercent", animSpeedPercent, speedSmoothTime, Time.deltaTime);
    }
}
