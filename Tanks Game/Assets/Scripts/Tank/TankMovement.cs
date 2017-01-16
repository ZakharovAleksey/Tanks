using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerID = 1;
    public float m_MoveSpeed = 10f;
    public float m_RotationSpeed = 60f;

    private Rigidbody m_RigitBody;
    private float m_Move;
    private float m_Rotate;

	// Use this for initialization
	void Start ()
    {
        m_RigitBody = GetComponent<Rigidbody>();
	}


    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 offset = m_Move * transform.forward * m_MoveSpeed * Time.deltaTime;
        m_RigitBody.MovePosition(m_RigitBody.position + offset);
    }

    private void Rotate()
    {
        float rotationAngle = m_Rotate * m_RotationSpeed * Time.deltaTime;
        Quaternion rotationOffset = Quaternion.Euler(0f, rotationAngle, 0f);

        m_RigitBody.MoveRotation(m_RigitBody.rotation * rotationOffset);
    }

    // Update is called once per frame
    void Update ()
    {
        m_Move = Input.GetAxis("Vertical_" + m_PlayerID.ToString());
        m_Rotate = Input.GetAxis("Horizontal_" + m_PlayerID.ToString());
	}
}
