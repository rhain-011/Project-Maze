using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    [SerializeField] bool isWalking;
    [SerializeField] bool isRunning;
    [SerializeField] bool isJumping;
    [SerializeField] bool grounded;
    public Transform groundCheck;
    public LayerMask groundedMask;
    public Animator anim;

    private PlayerStatManager playerStat;
    private Rigidbody rb;
    private float walkSpeed;
    private float sprintSpeed;
    private float jumpForce;
    private float fallMultiplier;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 targetMoveAmount;
    //public Camera tdCamera; // main camera reference

    // Start is called before the first frame update
    void Awake()
    {
        playerStat = GetComponent<PlayerStatManager>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

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
        moveDir = new Vector3(inputX, 0, inputY);

        // checks if player is on the ground
        Ray ray = new Ray(groundCheck.transform.position, -groundCheck.transform.up);
        Debug.DrawRay(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.08f, groundedMask))
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            //Debug.Log("TargerMovAmount = " + targetMoveAmount.magnitude);
            isWalking = true;
            targetMoveAmount = moveDir * walkSpeed;
            anim.SetBool("isWalking", isWalking);

            // run
            if (Input.GetKey("left shift"))
            {
                isRunning = true;
                targetMoveAmount = moveDir * sprintSpeed;
                anim.SetBool("isRunning", isRunning);
            }
            else
            {
                isRunning = false;
                //targetMoveAmount = moveDir * walkSpeed;
                anim.SetBool("isRunning", isRunning);
            }

            transform.Translate(targetMoveAmount);
        }
        else
        {
            isWalking = false;
            isRunning = false;
            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isRunning", isRunning);
        }
    }
}
