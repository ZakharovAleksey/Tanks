using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Доделать интерфейс

public class Bomb : MonoBehaviour
{
    public float m_Damage = 10f;
    public float m_TimeToExplosion = 2f;
    public float m_RespawnTime = 10f;
    
    public GameObject m_BombExplosionPrefab;

    protected ParticleSystem m_ExplosionParticles;
    protected AudioSource m_ExplosionAudio;


    protected void Awake()
    {
        m_ExplosionParticles = Instantiate(m_BombExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    // Loop for all players and check if it in explosion area - the hit him, otherwise do nothing
    virtual protected void Explosion()
    {
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play animation and sound
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(gameObject);
    }
}