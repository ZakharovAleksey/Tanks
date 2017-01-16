using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    public float m_FullHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_MiddleHealthColor = Color.yellow;
    public Color m_LowHealthColor = Color.red;
    public GameObject m_TankExplosionPrefab;


    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    private float m_CurrentHealth;
    private bool m_IsDead;

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_TankExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_FullHealth;
        m_IsDead = false;

        SetHealthUI();
    }


    private void FixedUpdate()
    {
        m_CurrentHealth -= 1f;
        SetHealthUI();
        if (m_CurrentHealth <= 0f && !m_IsDead)
        {
            OnDeath();
        }
    }

    private void SetHealthUI()
    {
        m_Slider.value = m_CurrentHealth;

        if (m_CurrentHealth >= 0.8 * m_FullHealth)
        {
            m_FillImage.color = m_FullHealthColor;
        }
        else if (m_CurrentHealth < 0.8 * m_FullHealth && m_CurrentHealth >= 0.3 * m_FullHealth)
        {
            m_FillImage.color = m_MiddleHealthColor;
        }
        else
        {
            m_FillImage.color = m_LowHealthColor;
        }
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;

        SetHealthUI();

        if (m_CurrentHealth <= 0f && !m_IsDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        m_IsDead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        m_ExplosionParticles.Play();

        // Play the tank explosion sound effect.
        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
    }

}
