using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    [SerializeField] bool grounded;
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
        // todo test, remove if
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            float inputX = Input.GetAxisRaw("Horizontal") * walkSpeed * Time.deltaTime;
            float inputY = Input.GetAxisRaw("Vertical") * walkSpeed * Time.deltaTime;
            moveDir = new Vector3(inputX, 0, inputY);
            anim.SetBool("isWalking", true);
        }
        else { anim.SetBool("isWalking", false); }

        //// run
        //if (Input.GetKey("left shift"))
        //{
        //    targetMoveAmount = moveDir * sprintSpeed;
        //    anim.SetBool("isWalking", false);
        //    anim.SetBool("isRunning", true);
        //}
        //else if(Input.GetKeyUp("left shift"))
        //{
        //    targetMoveAmount = moveDir * walkSpeed;
        //    anim.SetBool("isRunning", false);
        //    anim.SetBool("isWalking", true);
        //}
        //else
        //{
        //    anim.SetBool("isRunning", false);
        //    anim.SetBool("isWalking", false);
        //}

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
        Debug.Log("TargerMOvAmount = " + targetMoveAmount.normalized);
    }
    
}
