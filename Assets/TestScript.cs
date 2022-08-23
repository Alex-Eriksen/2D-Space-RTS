using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour, IScrollHandler
{
    public void OnScroll( PointerEventData eventData )
    {
        Debug.Log( eventData.scrollDelta );
    }
}
