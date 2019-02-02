using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    PlayerController fppController;
    ThirdPersonController tppController;
    FirstPersonCamera fppCamController;

    public GameObject ThirdPersonCam; // 0
    public GameObject FirstPersonCam; // 1
    //public GameObject TopDownCam;     // 2
    public int CamMode;

    void Start()
    {
        fppController = GetComponent<PlayerController>();
        fppCamController = GetComponent<FirstPersonCamera>();
        tppController = GetComponent<ThirdPersonController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ChangeCamera"))
        {
            if (CamMode == 1)
            {
                CamMode = 0;
            }
            else
            {
                CamMode += 1;
            }
        }
        
        StartCoroutine(CameraChange());

    }

    IEnumerator CameraChange()
    {
        yield return new WaitForSeconds(0.01f);
        if(CamMode == 0)
        {
            ThirdPersonCam.SetActive(false);
            tppController.enabled = false;
            FirstPersonCam.SetActive(true);
            fppCamController.enabled = true;
            fppController.enabled = true; 
        }
        if(CamMode == 1)
        {
            ThirdPersonCam.SetActive(true);
            tppController.enabled = true;
            FirstPersonCam.SetActive(false);
            fppCamController.enabled = false;
            fppController.enabled = false;
        }
    }
}
