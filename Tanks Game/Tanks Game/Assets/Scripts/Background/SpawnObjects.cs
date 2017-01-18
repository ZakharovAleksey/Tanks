using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject m_TurretPrefab;
    public Transform[] m_TurretSpawnPoints;

	// Use this for initialization
	void Start ()
    {

        SpawnTurrets();
	}

    private void SpawnTurrets()
    {
        for (int i = 0; i < m_TurretSpawnPoints.Length; ++i)
        {
            Instantiate(m_TurretPrefab, m_TurretSpawnPoints[i].position, Quaternion.identity);
        }
    }


}
