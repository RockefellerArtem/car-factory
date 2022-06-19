using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public bool IsPressed;
	[SerializeField] private GameObject _joystickGameObject;
	[SerializeField] private Image _joystick;
	[SerializeField] private Image _stick;
	private Vector2 _imputVector;

	[Space, Header("Settings")]
	[SerializeField] private float _magnitudeStick;

    public void OnDrag(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			_joystick.rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 position))
		{
			position.x /= _joystick.rectTransform.sizeDelta.x;
			position.y /= _joystick.rectTransform.sizeDelta.y;
			_imputVector = new Vector2(position.x, position.y);

			if (_imputVector.magnitude > _magnitudeStick)
			{
				_imputVector = _imputVector.normalized;
			}

			_stick.rectTransform.anchoredPosition = new Vector2 (
				_imputVector.x * (_joystick.rectTransform.sizeDelta.x / 2), 
				_imputVector.y * (_joystick.rectTransform.sizeDelta.y / 2));
		}
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		_joystickGameObject.transform.position = eventData.position;
		_joystickGameObject.SetActive(true);
		IsPressed = true;
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		_imputVector = Vector2.zero;
		_stick.rectTransform.anchoredPosition = Vector2.zero;
		_joystickGameObject.SetActive(false);
		IsPressed = false;
	}

	public float Horizontal()
	{
		if (_imputVector.x != 0)
		{
			return _imputVector.x;
		}
		else
		{
			return 0;
		}
	}
	public float Vertical()
	{
		if (_imputVector.y != 0)
		{
			return _imputVector.y;
		}
		else 
		{
			return 0;
		}
	}

}
