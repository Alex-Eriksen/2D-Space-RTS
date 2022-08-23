using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Image))]
public class Clickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISerializationCallbackReceiver, IPointerDownHandler, IPointerUpHandler
{
    public Image TargetGraphic;

    public float ColorChangeSpeed = 2.0f;

    public Color HighlightedColor = new Color( 255, 255, 255, 1 );
    public Color PressedColor = new Color ( 255, 255, 255, 0.5f );

    public UnityEvent OnClick;

    private Color m_startColor;
    private bool m_isHovering = false;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        TargetGraphic = GetComponent<Image>();
    }

    private void Start()
    {
        m_startColor = TargetGraphic.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_isHovering = true;
        StartCoroutine(SwapColors(HighlightedColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_isHovering = false;
        StartCoroutine(SwapColors(m_startColor));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(SwapColors(PressedColor));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClick?.Invoke();
        StartCoroutine(SwapColors(m_startColor));
    }

    private IEnumerator SwapColors(Color color)
    {
        TargetGraphic.color = Color.Lerp(TargetGraphic.color, color, ColorChangeSpeed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        if(TargetGraphic.color != color)
        {
            StartCoroutine(SwapColors(color));
        }
    }
}
