using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBomb : MonoBehaviour
{
    public float m_Damage = 5f;
    public float m_RespawnTime = 10f;
    public GameObject m_BombExplosionPrefab;

    private ParticleSystem m_ExplosionParticles;
    private AudioSource m_ExplosionAudio;

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_BombExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TankHealth th = other.gameObject.GetComponent<TankHealth>();
            th.TakeDamage(m_Damage);

            Explosion();    
        }
    }


    private void Explosion()
    {
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Execute animation and audio of explosion
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(gameObject);
    }
}
