using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Vector3 objectPos;
    float distance;
    public float throwForce = 500f;

    public bool isHolding = false;
    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    Rigidbody itemRb;

    void Start()
    {
        itemRb = item.GetComponent<Rigidbody>();    
    }


    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(item.transform.position, tempParent.transform.position);
        if(distance >= 1f)
        {
            isHolding = false;
        }

        if (isHolding)
        {
            itemRb.velocity = Vector3.zero;
            itemRb.angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

            if (Input.GetMouseButton(1))
            {
                //throw
                itemRb.AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            itemRb.useGravity = true;
            item.transform.position = objectPos;
        }
    }

    void OnMouseDown()
    {
        if (distance <= 1f)
        {
            isHolding = true;
            itemRb.useGravity = false;
            itemRb.detectCollisions = true;
        }
    }

    void OnMouseUp()
    {
        isHolding = false;
    }
}
