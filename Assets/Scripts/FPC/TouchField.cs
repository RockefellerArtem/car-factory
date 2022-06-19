using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 TouchDist;
    public bool IsPressed;
    private Vector2 _pointerOld;
    private int _pointerId;
    private bool _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        _pointerId = eventData.pointerId;
        _pointerOld = eventData.position;
    }

    private void FixedUpdate()
    {
        if (IsPressed)
        {
            if (_pointerId >= 0 && _pointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[_pointerId].position - _pointerOld;
                _pointerOld = Input.touches[_pointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                _pointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }
}
