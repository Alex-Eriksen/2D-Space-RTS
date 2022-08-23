using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StarSystemEventSystem : MonoBehaviour, IPointerClickHandler
{
    private GraphicRaycaster m_raycaster;
    private PointerEventData m_pointerEventData;
    private EventSystem m_eventSystem;
    private GameObject m_gameObject;

    private void Start()
    {
        m_raycaster = GetComponent<GraphicRaycaster>();
        m_eventSystem = GetComponent<EventSystem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();

        m_raycaster.Raycast(eventData, results);

        foreach(RaycastResult result in results)
        {
            Debug.Log(result);
        }
    }
}
