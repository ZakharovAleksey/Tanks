using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject m_TurretPrefab;
    public Transform[] m_TurretSpawnPoints;

    public int m_MedicineSpawnCount = 1;
    public GameObject m_MedicinePrefab;
    public Transform[] m_MedicineSpawnPoints;

    public GameObject m_TimerBombPrefab;
    public GameObject m_PlasticBombPrefab;
    public Transform[] m_BombSpawnPoints;

    private GameObject[] m_Turrets;
    private GameObject[] m_Bombs;
    private GameObject[] m_Medicine;

    private void Start()
    {
        m_Turrets = new GameObject[m_TurretSpawnPoints.Length];
        m_Bombs = new GameObject[m_BombSpawnPoints.Length];
        m_Medicine = new GameObject[m_MedicineSpawnPoints.Length];
    }

    // Use this for initialization
    public void Spawn()
    {
        InvokeRepeating("SpawnMedicine", 0f, Medicine.m_RespawnTime);
        InvokeRepeating("SpawnBomb", 0f, Bomb.m_RespawnTime);
        SpawnTurrets();
	}

    public void Remove()
    {
        for (int i = 0; i < m_Turrets.Length; ++i)
            m_Turrets[i].SetActive(false);

        for (int i = 0; i < m_Bombs.Length; ++i)
        {
            if(m_Bombs[i] != null)
                m_Bombs[i].SetActive(false);
        }

        for (int i = 0; i < m_Medicine.Length; ++i)
        {
            if (m_Medicine[i] != null)
                m_Medicine[i].SetActive(false);
        }
    }

    private void SpawnTurrets()
    {
        for (int i = 0; i < m_TurretSpawnPoints.Length; ++i)
        {
            if (m_Turrets[i] == null || !m_Turrets[i].activeSelf)
                m_Turrets[i] = Instantiate(m_TurretPrefab, m_TurretSpawnPoints[i].position, Quaternion.identity) as GameObject;
        }
    }

    private void SpawnMedicine()
    {
        for (int i = 0; i < m_MedicineSpawnCount; ++i)
        {
            int spawnPointID = Random.Range(0, m_MedicineSpawnPoints.Length);
            if (m_Medicine[spawnPointID] == null || !m_Medicine[spawnPointID].activeSelf)
            {
                m_Medicine[spawnPointID] = Instantiate(m_MedicinePrefab, m_MedicineSpawnPoints[spawnPointID].position, Quaternion.identity) as GameObject;
            }
        }
    }

    private void SpawnBomb()
    {
        for (int i = 0; i < m_BombSpawnPoints.Length; ++i)
        {
            if (m_Bombs[i] == null || !m_Bombs[i].activeSelf)
            {
                int rand = Random.Range(0, 10);
                if (rand % 2 == 0)
                {
                    m_Bombs[i] = Instantiate(m_TimerBombPrefab, m_BombSpawnPoints[i].position, Quaternion.identity) as GameObject;
                }
                else
                {
                    m_Bombs[i] = Instantiate(m_PlasticBombPrefab, m_BombSpawnPoints[i].position, Quaternion.identity) as GameObject;
                }
            }
        }
    }

}
