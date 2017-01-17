using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public float m_AffectedRadius = 10f;
    public float m_RotationSpeed = 50f;
    public Transform m_ShellSpawnPosition;
    public GameObject m_ShellPrefab;
    public GameObject m_TowerPrefab;

    private bool m_IsTargetInArea = false;
    private GameObject m_Target;
    private Vector3 m_LastTargetPosition;
    private float m_ShellRechargeTime;
    private float m_ShellShootForce;
    private float m_TimeSinceLastShoot = 0f;


    private void Awake()
    {
        SetShell();
    }

    private void FixedUpdate()
    {
        CheckOnTarget();

        if (m_IsTargetInArea)
        {
            RotateTowardsThePlayer();
            Shoot();
        }
    }

    private void CheckOnTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_AffectedRadius);
        bool isInAreaAtCurTime = false;

        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].gameObject.CompareTag("Player"))
            {
                m_Target = colliders[i].gameObject;
                m_LastTargetPosition = m_Target.transform.position;
                isInAreaAtCurTime = true;
            }
        }

        m_IsTargetInArea = isInAreaAtCurTime;
    }

    private void RotateTowardsThePlayer()
    {
        Quaternion angle = Quaternion.LookRotation(m_LastTargetPosition - transform.position);
        // Because the download prefab has a bad rotation we need extra correction angle to be included
        //{
        //    float correctionAngle = angle.eulerAngles.y;
        //    correctionAngle -= 90f;
        //    angle = Quaternion.Euler(angle.eulerAngles.x, correctionAngle, angle.eulerAngles.z);
        //}
        if (transform.rotation != angle)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, m_RotationSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        m_TimeSinceLastShoot += Time.deltaTime;

        if (m_TimeSinceLastShoot >= m_ShellRechargeTime)
        {
            m_TimeSinceLastShoot = 0f;

            GameObject shell = Instantiate(m_ShellPrefab, m_ShellSpawnPosition.position, m_ShellSpawnPosition.rotation) as GameObject;

            Rigidbody shellRB = shell.GetComponent<Rigidbody>();

            shellRB.AddForce(m_ShellSpawnPosition.forward * m_ShellShootForce);

        }
    }

    private void SetShell()
    {
        ShellParameters sp = m_ShellPrefab.GetComponent<ShellParameters>();

        m_ShellRechargeTime = sp.m_RechargeTime;
        m_ShellShootForce = sp.m_ShootForce;
    }

}

