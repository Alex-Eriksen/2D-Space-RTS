using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;
using TMPro;
using System;

public class ClickablePlanet : MonoBehaviour
{
    public Planet planet;
    private TextMeshProUGUI m_planetNameUI;
    private Transform m_canvasTransform;
    private RectTransform m_canvasRectTransform;
    private Action<float> m_handlerRef;

    private void OnDestroy()
    {
        Uninitialize( ref m_handlerRef );
    }

    public void Initialize( float initialZoomDistance, ref Action<float> handler )
    {
        handler += OnZoom;
        m_handlerRef = handler;

        m_planetNameUI = GetComponentInChildren<TextMeshProUGUI>();
        m_canvasTransform = m_planetNameUI.transform.parent.transform;
        m_canvasRectTransform = m_canvasTransform.GetComponent<RectTransform>();
        m_planetNameUI.text = planet.planetName;

        OnZoom( initialZoomDistance );
    }

    public void Uninitialize( ref Action<float> handler )
    {
        handler -= OnZoom;
    }

    private void OnZoom( float zoomLevel )
    {
        float size = StarlightUtils.CalculateApparentSize( planet.Radius * 2 * (float) Config.GetDecimal( "EARTH_RADIUS" ), zoomLevel );
        m_canvasTransform.localPosition = new Vector3( 0, size, 0 );
        m_canvasRectTransform.sizeDelta = new Vector2( zoomLevel / (Config.GetFloat( "STAR_ORBIT_DISTANCE" ) * 2), zoomLevel / (Config.GetFloat( "STAR_ORBIT_DISTANCE" ) * 6) );
    }

    public void OnClick()
    {
        Debug.Log("ClickableStar::OnClick -- " + planet.planetName);
        var obj = Instantiate(Resources.Load<GameObject>("UIPrefabs/View/PlanetView"));
        obj.GetComponent<PlanetView>().Planet = planet;
        ViewManager.Instance.Open(obj, planet.planetName);
    }
}
