using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIViewTab : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string tabName = "No Name";

    public UIView myUIView;

    public GameObject content;

    public bool InView = true;

    private Canvas m_canvas;
    private RectTransform m_myRectTransform, m_myContentRectTransform;
    private Vector2 m_myStartPosition, m_myContentStartPosition;

    private void Start()
    {
        if(myUIView == null)
        {
            Destroy(content);
            Destroy(gameObject);
        }

        m_myRectTransform = GetComponent<RectTransform>();
        m_myContentRectTransform = content.GetComponent<RectTransform>();
        m_canvas = GetComponentInParent<Canvas>();

        GetComponentInChildren<TextMeshProUGUI>().text = tabName;
    }

    private void OnDestroy()
    {
        Destroy( content );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myUIView.SwitchTo(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_myStartPosition = m_myRectTransform.anchoredPosition;
        m_myContentStartPosition = m_myContentRectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_myRectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor;
        m_myContentRectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(m_myRectTransform.anchoredPosition.y > ((RectTransform) m_myRectTransform.parent).anchoredPosition.y);
        //if(!((RectTransform)m_myRectTransform.parent).rect)
        //{
        //    Debug.Log("UIViewTab::OnEndDrag - Open a new view.");
        //}

        // Check if the tab is outside of the UIView, if it is create a new UIView.
        m_myRectTransform.anchoredPosition = m_myStartPosition;
        m_myContentRectTransform.anchoredPosition = m_myContentStartPosition;
    }
}
