using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveShellParameters : ShellParameters
{
    public float m_ExplosionRadius = 3f;

    protected override void Awake()
    {
        m_Damage *= 2f;
        m_RechargeTime *= 2f;
        m_ShootForce = 1200f;

        base.Awake();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, m_ExplosionRadius);

        for (int colliderID = 0; colliderID < collisions.Length; ++colliderID)
        {
            if (collisions[colliderID].gameObject.CompareTag("Player"))
            {
                TankHealth th = collisions[colliderID].gameObject.GetComponent<TankHealth>();
                th.TakeDamage(m_Damage);
            }
        }

        Explosion();
    }
}
