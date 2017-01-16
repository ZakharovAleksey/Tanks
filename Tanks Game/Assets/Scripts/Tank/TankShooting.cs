using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public GameObject[] m_ShellPrefabs;
    public Transform m_ShellSpawnPoint;

    private string m_PlayerShootBTN;
    private string m_PlayerChangeShellBTN;
    private float m_TimeSinceLastShoot = 0f;

    private GameObject m_CurShellPrefab = null;
    private int m_CurShellID = 0;
    private float m_CurShellDamage;
    private float m_CurShellShootForce;
    private float m_CurShellRechargeTime;



	// Use this for initialization
	void Start ()
    {
        SwitchPlayerID();
        SetChoosenShell();
	}

    /// <summary>
    /// Choose Shoot Button depending on player ID
    /// </summary>
    private void SwitchPlayerID()
    {
        TankMovement tankParam = GetComponent<TankMovement>();

        switch (tankParam.m_PlayerID)
        {
            case 1:
                m_PlayerShootBTN = "Fire_1";
                m_PlayerChangeShellBTN = "SwapShell_1";
                break;
            case 2:
                m_PlayerShootBTN = "Fire_2";
                m_PlayerChangeShellBTN = "SwapShell_2";
                break;
            default:
                print("Error in choosen player ID");
                break;
        }
    }

    #region Update

    // Update is called once per frame
    void Update ()
    {
        m_TimeSinceLastShoot += Time.deltaTime;

        if (Input.GetButtonDown(m_PlayerShootBTN) && m_TimeSinceLastShoot >= m_CurShellRechargeTime)
        {
            print("Cur shell id: = " + m_CurShellID);
            Shoot();
        }
        else if (Input.GetButtonDown(m_PlayerChangeShellBTN) && m_TimeSinceLastShoot >= m_CurShellRechargeTime)
        {
            SetNextShell();
        }
	}

    /// <summary>
    /// Shoot procedure implementation
    /// </summary>
    private void Shoot()
    {
        // Set Shoot timer to zero
        m_TimeSinceLastShoot = 0f;

        // Create current shell prefab and it's own movement
        GameObject shell = Instantiate(m_CurShellPrefab, m_ShellSpawnPoint.position, m_ShellSpawnPoint.rotation) as GameObject;

        Rigidbody shellRB = shell.GetComponent<Rigidbody>();
        shellRB.AddForce(m_ShellSpawnPoint.forward * m_CurShellShootForce);
    }

    /// <summary>
    /// Set next shell in queue (In this implementation onlly two shell types: middle distance and long distance)
    /// </summary>
    private void SetNextShell()
    {
        // Obtain next shell id
        m_CurShellID = (m_CurShellID + 1 >= m_ShellPrefabs.Length) ? 0 : m_CurShellID + 1;
        SetChoosenShell();
    }

    #endregion

    /// <summary>
    /// Initilize current shell and store it's parameters to apropriate fields
    /// </summary>
    private void SetChoosenShell()
    {
        m_CurShellPrefab = m_ShellPrefabs[m_CurShellID];

        ShellParameters shellParam = m_CurShellPrefab.GetComponent<ShellParameters>();
        m_CurShellDamage = shellParam.m_Damage;
        m_CurShellShootForce = shellParam.m_ShootForce;
        m_CurShellRechargeTime = shellParam.m_RechargeTime;
    }
}
