using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TppMovementInput : MonoBehaviour
{
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    [SerializeField] bool blockRotationPlayer = false;
    public float desiredRotationSpeed;
    public float speed;
    public float allowPlayerRotation;
    public Camera tppCam;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
    }

    void PlayerRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 forward = tppCam.transform.forward;
        Vector3 right = tppCam.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;
        

        if(blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
    }

    //controls the animation
    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        // change 0.0f to delay animation transition
        anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2.0f);
        anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2.0f);

        speed = new Vector2(InputX, InputZ).sqrMagnitude;
        // physically move the player
        if(speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        }
    }
}
