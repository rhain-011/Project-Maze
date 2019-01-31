using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform m_Player;
    public float m_SmoothSpeed = 0.5f;
    public float m_Heigth = 10f;
    public float zOffset = 5.0f;

    private Vector3 m_Velocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {

        Vector3 pos = new Vector3
        {
            x = m_Player.position.x,
            z = m_Player.position.z - zOffset,
            y = m_Player.position.y + m_Heigth
        };
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref m_Velocity, m_SmoothSpeed);

    }
}
