using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLocalization : MonoBehaviour
{
	[SerializeField] private Languages _language;
	[SerializeField] private GameObject _outlineGameObject;
	private void Start()
	{
		SwutchOutlineButton();

		LocalizationManager.Instance.onLocalizationAction += SwutchOutlineButton;

		GetComponent<Button>().onClick.AddListener(() =>
		{
			LocalizationManager.Instance.SetLanguage(_language);
		});
	}

	private void SwutchOutlineButton()
	{
		if (LocalizationManager.Instance.CurrentLanguage == _language) _outlineGameObject.SetActive(true);
		else _outlineGameObject.SetActive(false);
	}
}
