using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;
using System;

public class StarGraphics : MonoBehaviour
{
    private Star m_star;
    private Transform m_transform;
    private SpriteRenderer m_spriteRenderer;
    private Action<float> m_handlerRef;

    private void OnDestroy()
    {
        Uninitialize( ref m_handlerRef );
    }

    public void Initialize( Star star, float initialZoomDistance, ref Action<float> handler )
    {
        if (m_star != null)
        {
            return;
        }

        m_transform = transform;
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        handler += OnZoom;
        m_handlerRef = handler;

        m_star = star;

        m_spriteRenderer.color = m_star.Color;

        float size = StarlightUtils.CalculateApparentSize( m_star.Radius * 2 * (float) Config.GetDecimal( "SOL_RADIUS" ) , initialZoomDistance );

        m_transform.localScale = new Vector3( size, size, size );
    }

    public void Uninitialize( ref Action<float> handler )
    {
        handler -= OnZoom;
    }

    private void OnZoom( float zoomLevel )
    {
        float size = StarlightUtils.CalculateApparentSize( m_star.Radius * 2 * (float) Config.GetDecimal( "SOL_RADIUS" ), zoomLevel );

        m_transform.localScale = new Vector3( size, size, size );
    }
}
