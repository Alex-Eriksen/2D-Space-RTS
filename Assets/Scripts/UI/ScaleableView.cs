using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleableView : MonoBehaviour, IDragHandler
{
    private Canvas m_canvas;
    [SerializeField] private RectTransform m_rect;
    [SerializeField] private float m_minWidth;
    [SerializeField] private float m_minHeight;

    public bool IsRight = false;

    private void Awake()
    {
        m_canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_rect.sizeDelta = Vector2.zero;
        m_rect.anchoredPosition = Vector2.zero;

        float x = eventData.position.x / Screen.width;
        float y = eventData.position.y / Screen.height;

        if (IsRight)
        {
            m_rect.anchorMin = new Vector2(m_rect.anchorMin.x, y);
            m_rect.anchorMax = new Vector2(x, m_rect.anchorMax.y);

            if (m_rect.anchorMax.x - m_rect.anchorMin.x < m_minWidth)
            {
                m_rect.anchorMax = new Vector2(m_rect.anchorMin.x + m_minWidth, m_rect.anchorMax.y);
            }

            if(m_rect.anchorMax.y - m_rect.anchorMin.y < m_minHeight)
            {
                m_rect.anchorMin = new Vector2(m_rect.anchorMin.x, m_rect.anchorMax.y - m_minHeight);
            }
        }
        else
        {
            m_rect.anchorMin = new Vector2(x, y);
            m_rect.anchorMax = new Vector2(m_rect.anchorMax.x, m_rect.anchorMax.y);

            if (m_rect.anchorMax.x - m_rect.anchorMin.x < m_minWidth)
            {
                m_rect.anchorMin = new Vector2(m_rect.anchorMax.x - m_minWidth, m_rect.anchorMin.y);
            }

            if (m_rect.anchorMax.y - m_rect.anchorMin.y < m_minHeight)
            {
                m_rect.anchorMin = new Vector2(m_rect.anchorMin.x, m_rect.anchorMax.y - m_minHeight);
            }
        }
    }
}
