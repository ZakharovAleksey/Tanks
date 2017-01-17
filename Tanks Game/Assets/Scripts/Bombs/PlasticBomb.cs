using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBomb : Bomb
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TankHealth th = other.gameObject.GetComponent<TankHealth>();
            th.TakeDamage(m_Damage);

            Explosion();    
        }
    }


    override protected void Explosion()
    {
        base.Explosion();
    }
}
