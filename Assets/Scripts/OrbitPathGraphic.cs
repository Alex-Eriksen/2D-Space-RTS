using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPathGraphic : MonoBehaviour
{
    private LineRenderer m_lineRenderer;

    public float radius = 2f;

    private int m_numPoints = 360;

    public void GeneratePath()
    {
        m_lineRenderer = GetComponent<LineRenderer>();

        m_lineRenderer.positionCount = m_numPoints;

        for (var i = 0; i < m_numPoints; i++)
        {
            var angle = (Mathf.PI * 2f) * ((float) i / m_numPoints);
            var point = new Vector3( Mathf.Sin( angle ) * radius, Mathf.Cos( angle ) * radius, 0f );
            m_lineRenderer.SetPosition(i, point );
        }
    }
}
