using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableView : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform m_rect;

    public void OnDrag(PointerEventData eventData)
    {
        float x = eventData.delta.x / Screen.width;
        float y = eventData.delta.y / Screen.height;

        m_rect.anchorMax = new Vector2(m_rect.anchorMax.x + x, m_rect.anchorMax.y + y);
        m_rect.anchorMin = new Vector2(m_rect.anchorMin.x + x, m_rect.anchorMin.y + y);
    }
}
