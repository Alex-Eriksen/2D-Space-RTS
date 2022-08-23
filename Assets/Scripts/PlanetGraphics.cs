using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;
using System;

public class PlanetGraphics : MonoBehaviour
{
    private Planet m_planet;
    private Transform m_transform;
    private SpriteRenderer m_spriteRenderer;
    private Action<float> m_handlerRef;

    private void OnDestroy()
    {
        Uninitialize( ref m_handlerRef );
    }

    public void Initialize( Planet planet, float initialZoomDistance, ref Action<float> handler )
    {
        m_transform = transform;
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        handler += OnZoom;
        m_handlerRef = handler;

        m_planet = planet;

        m_spriteRenderer.color = m_planet.Color;

        float size = StarlightUtils.CalculateApparentSize( m_planet.Radius * 2 * (float) Config.GetDecimal( "EARTH_RADIUS" ), initialZoomDistance );

        m_transform.localScale = new Vector3( size, size, size );
    }

    public void Uninitialize( ref Action<float> handler )
    {
        handler -= OnZoom;
    }

    private void OnZoom( float zoomLevel )
    {
        float size = StarlightUtils.CalculateApparentSize( m_planet.Radius * 2 * (float) Config.GetDecimal( "EARTH_RADIUS" ), zoomLevel );

        m_transform.localScale = new Vector3( size, size, size );
    }
}
