using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // Character Stats
    public float p_Health = 100f;
    public float p_Stamina = 50.0f;

    // Movement Stats
    public float p_DefaultWalkSpeed = 1.0f;
    public float p_DefaultRunSpeed = 2.0f;
    public float p_DefaultJumForce = 200.0f;
    public float p_DefaultFallMultiplier = 2.5f;
}
