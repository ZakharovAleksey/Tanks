using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    public float m_TerrainHeight = 10f;
    public float m_FenceOffset = 2.75f;
    public GameObject m_FencePrefab;
    
    private int m_FinceWidthCount = 20;
    private int m_FenceHeightCount = 18;

	void Start ()
    {
        TopBoundary();
        BottomBoundary();
        RightBoundary();
        LeftBoundary();

    }

    private void TopBoundary()
    {
        Vector3 curPosition = new Vector3(32.5f, m_TerrainHeight, 10f);

        for (int i = 0; i < m_FinceWidthCount; ++i)
        {
            Instantiate(m_FencePrefab, curPosition, Quaternion.identity);
            curPosition -= new Vector3(m_FenceOffset, 0f, 0f);
        }
    }

    private void BottomBoundary()
    {
        Vector3 curPosition = new Vector3(32.5f, m_TerrainHeight, -39f);

        for (int i = 0; i < m_FinceWidthCount / 2; ++i)
        {
            Instantiate(m_FencePrefab, curPosition, Quaternion.identity);
            curPosition -= new Vector3(m_FenceOffset, 0f, 0f);
        }
    }

    private void RightBoundary()
    {
        Vector3 curPosition = new Vector3(32.5f, m_TerrainHeight, 7.75f);

        for (int i = 0; i < m_FenceHeightCount; ++i)
        {
            Instantiate(m_FencePrefab, curPosition, Quaternion.Euler(0f, 90f, 0f));
            curPosition -= new Vector3(0f, 0f, m_FenceOffset);
        }
    }

    private void LeftBoundary()
    {
        Vector3 curPosition = new Vector3(-22.5f, m_TerrainHeight, 7.75f);

        for (int i = 0; i < m_FenceHeightCount / 2; ++i)
        {
            Instantiate(m_FencePrefab, curPosition, Quaternion.Euler(0f, 90f, 0f));
            curPosition -= new Vector3(0f, 0f, m_FenceOffset);
        }
    }

}
