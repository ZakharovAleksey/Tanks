using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerID = 1;
    public float m_MoveSpeed = 10f;
    public float m_RotationSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIDLE;
    public AudioClip m_EngineDriving;

    private Rigidbody m_RigitBody;
    private float m_Move;
    private float m_Rotate;

    // Можно  добавить степень изменения высоты тона при замедлении/ускорении Audio Clip’а
    
    private void Awake()
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

        EngineAudio();
	}

    /// <summary>
    /// Play apropriate audio clip depending on type of tank movement
    /// </summary>
    private void EngineAudio()
    {
        if (Mathf.Abs(m_Move) < 0.1f && Mathf.Abs(m_Rotate) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIDLE;
                // Тут так как мы поменяли уровень - аудио останавливается и мы его запускаем
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIDLE)
            {
                m_MovementAudio.clip = m_EngineDriving;
                // Тут так как мы поменяли уровень - аудио останавливается и мы его запускаем
                m_MovementAudio.Play();
            }
        }
    }
}
