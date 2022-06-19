using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _gasolineText;
    [SerializeField] private TMP_Text _doorsText;
    [SerializeField] private TMP_Text _powerText;

    public Test2.Car Car { get; private set; }
    public Test2.Gasoline Gasoline { get; private set; }
    public Test2.Doors Doors { get; private set; }
    public Test2.Powers Powers { get; private set; }

    public int SO { get; private set; }
    public int ColorNumber { get; private set; }

    [SerializeField] private Button _select;

    public GameObject Obvodka;

    [SerializeField] private Image _image;

    public void Init(Test2.Car car,Test2.Gasoline gasoline,Test2.Doors doors,Test2.Powers powers, Sprite texture, Test2 test2, int so, int colorNumber)
    {
        Car = car;
        Gasoline = gasoline;
        Doors = doors;
        Powers = powers;
        SO = so;
        ColorNumber = colorNumber;

        _nameText.text = car.Name;
        _gasolineText.text = gasoline.Gasl.ToString();
        _doorsText.text = doors.Door.ToString();
        _powerText.text = powers.Power.ToString();

        _image.sprite = texture;

        _select.onClick.AddListener(() => test2.SelectObject(this));
    }
}
