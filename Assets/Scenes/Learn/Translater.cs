using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Translater : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private Text _nameautoText;
    [SerializeField] private Text _gasolineText;
    [SerializeField] private Text _doorsText;
    [SerializeField] private Text _lsText;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_Text _listauto;
    [SerializeField] private TMP_Text _factoryAuto;

    [Header("Buttons")]
    [SerializeField] private TMP_Text _clear;
    [SerializeField] private TMP_Text _delete;
    [SerializeField] private TMP_Text _create;

    private GameObject _select;

    private bool _isSelect;

    private void Start()
    {
        int language = PlayerPrefs.GetInt("language", 0);

        if (language == 0)
        {
            TranslateRU();
        }
        else
        {
            TranslateEN();
        }
    }
    public void TranslateEN()
    {
        Dictionary<int, string> en = new Dictionary<int, string>()
        {
            [0] = "New Auto",
            [1] = "Gasoline",
            [2] = "Doors",
            [3] = "LS",
            [4] = "Count",
            [5] = "Clear",
            [6] = "Delete",
            [7] = "Create",
            [8] = "List Auto.Count",
            [9] = $"Factory auto. v. {Application.version}  by Itibsoft "
        };

        _nameautoText.text = en[0];
        _gasolineText.text = en[1];
        _doorsText.text = en[2];
        _lsText.text = en[3];
        _countText.text = en[4];
        _clear.text = en[5];
        _delete.text = en[6];
        _create.text = en[7];
        _listauto.text = en[8];
        _factoryAuto.text = en[9];

        PlayerPrefs.SetInt("language", 1);
    }

    public void TranslateRU()
    {
        Dictionary<int, string> ru = new Dictionary<int, string>()
        {
            [0] = "Название авто:",
            [1] = "Бензин",
            [2] = "Двери",
            [3] = "ЛС",
            [4] = "Количество",
            [5] = "Очистить",
            [6] = "Удалить",
            [7] = "Создать",
            [8] = "СПИСОК МАШИН. Количество:     шт",
            [9] = $"Конвеер машин. v.{Application.version}  by Itibsoft "
    };

        _nameautoText.text = ru[0];
        _gasolineText.text = ru[1];
        _doorsText.text = ru[2];
        _lsText.text = ru[3];
        _countText.text = ru[4];
        _clear.text = ru[5];
        _delete.text = ru[6];
        _create.text = ru[7];
        _listauto.text = ru[8];
        _factoryAuto.text = ru[9];

        PlayerPrefs.SetInt("language", 0);
    }

    public void Selected(GameObject select)
    {
        _select?.SetActive(false);

        if (_isSelect || _select != select)
        {
            _select = select;
            select.SetActive(true);

            _isSelect = false;
        }

        else if (_isSelect == true)
        {
            select.SetActive(false);

            _isSelect = true;
        }
    }
}
