using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public Transform FPPCamera; // holds the camera object
    public LayerMask Raymask; // layer filter
    public float rayLength = 3f;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            if(Physics.Raycast(FPPCamera.position, FPPCamera.forward, out hit, rayLength, Raymask))
            {
                if(hit.transform.tag == "PickableObject")
                {

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

        }

    }




}
