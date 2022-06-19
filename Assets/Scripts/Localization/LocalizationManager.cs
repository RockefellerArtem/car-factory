using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LocalizationManager : MonoBehaviour
{
	public static LocalizationManager Instance { get; private set; }

    [SerializeField] private List<TextDataWord> _textsDataWords = new List<TextDataWord>();
    private Dictionary<string, TextDataWord> _dataTextsIds = new Dictionary<string, TextDataWord>();
    public Languages CurrentLanguage { get { return _currentLanguage; } private set { _currentLanguage = value; } }
	[SerializeField] private Languages _currentLanguage;

	public Action onLocalizationAction { get; set; }

	private void Awake()
	{
		Instance = this;

		string language = PlayerPrefs.GetString("CurrentLanguage", Languages.Russian.ToString());
		Languages currentLanguage = (Languages) (Enum.Parse(typeof(Languages), language));

		SetLanguage(currentLanguage);

		foreach (TextDataWord dataText in _textsDataWords)
		{
			_dataTextsIds.Add(dataText.Id, dataText);
		}
	}

	public void SetLanguage(Languages language)
	{
        CurrentLanguage = language;
		PlayerPrefs.SetString("CurrentLanguage", language.ToString());
		onLocalizationAction?.Invoke();
	}

    public string Localize(string id)
	{
		if (_dataTextsIds.ContainsKey(id) == true)
		{
			switch (CurrentLanguage)
			{
				case Languages.Russian: return _dataTextsIds[id].RU;
				case Languages.English: return _dataTextsIds[id].EN;
			}
		}

		return "NULL";
	}
}
