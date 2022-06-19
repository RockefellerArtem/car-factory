using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour
{
	[SerializeField] private string _id;
	private TMP_Text _textTMP;
	private Text _textDefault;

	public void Start()
	{
		LocalizationManager.Instance.onLocalizationAction += Localize;

		var defaultText = GetComponent<Text>();
		var textTMP = GetComponent<TMP_Text>();

		if(defaultText == null) _textTMP = textTMP;
		else _textDefault = defaultText;

		Localize();
	}

	private void Localize()
	{
		if (_textTMP != null) _textTMP.text = LocalizationManager.Instance.Localize(_id);
		if (_textDefault != null) _textDefault.text = LocalizationManager.Instance.Localize(_id);
	}
}
