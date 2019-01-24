// Amardeep Seeboruth
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flashlight : MonoBehaviour
{   //main flashlight
    public Light flashlight;
    //determine whether on or off
    public bool triggerFlashlight;
    //battery of the flashlight
    public float flashlightBattery = 100f;


    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponent<Light>();
        triggerFlashlight = false;
    }



    // Update is called once per frame
    void Update()
    {
        flashlight.enabled = triggerFlashlight;
        //press "g" to turn on or off 
        if (Input.GetKeyDown("g"))
        {
            triggerFlashlight = !triggerFlashlight;
        }

        if (triggerFlashlight)
        {
            flashlightBattery -= Time.time * 0.0009f; // reduce battery by 0.01
            if (flashlightBattery <= 0f)
            {
                triggerFlashlight = false;
            }
            
            else if (flashlightBattery <= 20)
            {
                flashlight.intensity -= Time.time * 0.00001f;
            }
        }
       
    }
}
