using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellParameters : MonoBehaviour
{
    public float m_Damage = 5f;
    public float m_LifeTime = 1f;
    public float m_RechargeTime = 0.3f;
    public float m_ShootForce = 1000f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, m_LifeTime);
    }
}
