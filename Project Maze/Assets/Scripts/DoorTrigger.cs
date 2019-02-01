using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{


    public GameObject door;
    bool Opened = false;
    public float distance = 0;


    void OnTriggerEnter(Collider collider)
    {

        if (!Opened)
        {
            Opened = true;
            door.transform.Translate(Vector3.down * distance); //* Time.deltaTime * distance);

        }
    }
}
