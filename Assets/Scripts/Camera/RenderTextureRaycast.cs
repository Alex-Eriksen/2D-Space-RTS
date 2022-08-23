using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RenderTextureRaycast : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Camera UICamera;
    [SerializeField] protected RectTransform RawImageRectTrans;
    [SerializeField] protected Camera RenderToTextureCamera;
    public LayerMask ClickablePlanetLayerMask;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Middle)
        {
            return;
        }

        Vector2 localPoint;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RawImageRectTrans, eventData.position, UICamera, out localPoint);
        
        Vector2 normalizedPoint = Rect.PointToNormalized(RawImageRectTrans.rect, localPoint);
        
        var renderRay = RenderToTextureCamera.ViewportPointToRay(normalizedPoint);

        // TODO: Fix this, it is not as I want it.
        if (Physics.Raycast(renderRay, out var raycastHit, Mathf.Infinity, ClickablePlanetLayerMask))
        {
            raycastHit.collider.GetComponentInParent<ClickablePlanet>().OnClick();
        }
        else
        {
            Debug.Log("No hit object");
        }
    }
}