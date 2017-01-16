using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBomb : MonoBehaviour
{

    public float m_Damage = 10f;
    public float m_TimeToExplosion = 2f;
    public float m_RespawnTime = 10f;
    public float m_ExplosionRadius = 500f;
    public GameObject[] m_Targets;
    public GameObject m_BombExplosionPrefab;

    private ParticleSystem m_ExplosionParticles;
    private AudioSource m_ExplosionAudio;
    private bool m_IsActive = false;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_BombExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

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

    // Determines if the player in the eplosion area
    private bool IsInAffectedArea(GameObject target)
    {
        float x = target.transform.position.x;
        float y = target.transform.position.y;

        // Determines if flayer in circle
        return Mathf.Pow((x - transform.position.x), 2f) + Mathf.Pow(y - transform.position.y, 2f) < Mathf.Pow(m_ExplosionRadius, 2f) ? true : false;
    }

    // Loop for all players and check if it in explosion area - the hit him, otherwise do nothing
    private void Explosion()
    {
        for (int targetID = 0; targetID < m_Targets.Length; ++targetID)
        {
            if (IsInAffectedArea(m_Targets[targetID]))
            {
                print("set damage");
                TankHealth th = m_Targets[targetID].GetComponent<TankHealth>();
                th.TakeDamage(m_Damage);
            }
        }

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play animation and sound
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(gameObject);
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
