using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularDiameter : MonoBehaviour
{
    public float distance = 100f;
    private float actualSize = 12_741f;
    private float apparentSize;

    private Transform m_transform;


    private void Start()
    {
        m_transform = transform;
    }

    private void Update()
    {
        apparentSize = CalculateApparentSize();

        m_transform.localScale = new Vector3( apparentSize, apparentSize, apparentSize );
    }

    private float CalculateApparentSize()
    {
        return 2 * Mathf.Atan( actualSize / (2 * distance) );
    }
}
