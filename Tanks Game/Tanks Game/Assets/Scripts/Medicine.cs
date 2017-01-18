using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public float m_Heal = 20f;
    static public float m_RespawnTime = 30f;
    public GameObject m_HealPrepab;

    private ParticleSystem m_HealParticleSystem;

	// Use this for initialization
	void Start ()
    {
        m_HealParticleSystem = Instantiate(m_HealPrepab).GetComponent<ParticleSystem>();
        m_HealParticleSystem.gameObject.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TankHealth th = other.gameObject.GetComponent<TankHealth>();
            th.TakeHeal(m_Heal);

            Heal(other);
        }
    }

    // Makes Heal Animation
    private void Heal(Collider other)
    {
        m_HealParticleSystem.transform.position = other.gameObject.transform.position;
        m_HealParticleSystem.gameObject.SetActive(true);

        m_HealParticleSystem.Play();

        Destroy(gameObject);
    }
}


