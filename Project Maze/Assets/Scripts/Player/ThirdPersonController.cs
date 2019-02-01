using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] bool isRunning;
    [SerializeField] bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundedMask;

    public float turnSmoothTime = 0.0f;
    public float speedSmoothTime = 0.0f;

    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;


    private float walkSpeed;
    private float sprintSpeed;
    private float jumpForce;
    private float fallMultiplier;

    public Animator anim;
    public Transform tppCam;
    private PlayerStatManager playerStat;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector2 inputDir = new Vector2(inputX, inputZ).normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + tppCam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }
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

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((isRunning) ? sprintSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animSpeedPercent = ((isRunning) ? 1 : 0.5f) * inputDir.magnitude;
        anim.SetFloat("speedPercent", animSpeedPercent, speedSmoothTime, Time.deltaTime);
    }
}
