using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBomb : Bomb//: MonoBehaviour
{
    public float m_ExplosionRadius = 5f;
 
    private bool m_IsActive = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f));

        if (m_IsActive)
        {
            // Set an explosion after elapsed explosion time
            Invoke("Explosion", m_TimeToExplosion);
        }
    }

    // Loop for all players and check if it in explosion area - the hit him, otherwise do nothing
    override protected void Explosion()
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

        base.Explosion();
    }

    // Checks if player enter the bomb
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_IsActive)
        {
            m_IsActive = true;
        }
    }
}
