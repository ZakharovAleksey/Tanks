using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShellParameters : MonoBehaviour
{
    public float m_Damage = 10f;
    public float m_LifeTime = 1f;
    public float m_RechargeTime = 0.3f;
    public float m_ShootForce = 1000f;
    public GameObject m_ShellExplosionPrefab;

    protected ParticleSystem m_ExplosionParticleSystem;
    protected AudioSource m_ExplosionAudio;

    virtual protected void Awake()
    {
        m_ExplosionParticleSystem = Instantiate(m_ShellExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionParticleSystem.gameObject.SetActive(false);

        m_ExplosionAudio = m_ExplosionParticleSystem.GetComponent<AudioSource>();   
    }

    protected void Start()
    {
        Invoke("Explosion", m_LifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
        if (collision.gameObject.CompareTag("Player"))
        {
            TankHealth th = collision.gameObject.GetComponent<TankHealth>();
            th.TakeDamage(m_Damage);
        }
    }

    // Execute the shell explosion audio and it's animation
    protected void Explosion()
    {
        m_ExplosionParticleSystem.transform.position = transform.position;
        m_ExplosionParticleSystem.gameObject.SetActive(true);
        m_ExplosionParticleSystem.Play();

        m_ExplosionAudio.Play();

        Destroy(gameObject);
    }

}
