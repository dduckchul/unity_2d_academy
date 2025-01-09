using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IDragHandler, IEndDragHandler

{
    public RectTransform Handle;
    public RectTransform rectTransform;

    public Vector2 joyStickInput;

    public float joyStickX;

    public void OnDrag(PointerEventData eventData)
    {
        joyStickInput = eventData.position - rectTransform.anchoredPosition;
        Handle.anchoredPosition = joyStickInput.normalized * 100;
        joyStickX = Handle.anchoredPosition.normalized.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Handle.anchoredPosition = Vector2.zero;
        joyStickX = 0;
    }
}
