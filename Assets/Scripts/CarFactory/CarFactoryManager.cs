using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using System.IO;


public partial class CarFactoryManager : MonoBehaviour
{
    private List<CarHolder> _carsHolder = new List<CarHolder>();

    [SerializeField] private List<CarTypeSO> _carTypesSO = new List<CarTypeSO>();

    [Header("Name")]
    [SerializeField] private TMP_InputField _inputName;

    [Header("Ñharacteristic")]
    [SerializeField] private TMP_InputField _inputGasoline;
    [SerializeField] private TMP_InputField _inputDoors;
    [SerializeField] private TMP_InputField _inputPower;

    [SerializeField] private TMP_Text _version;
    [SerializeField] private TMP_Text _autoText;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_InputField _inputCount;

    [SerializeField] private CarHolder _carHolderPrefab;
    [SerializeField] private Transform _context;

    private List<Car> _cars = new List<Car>();

    [SerializeField] private GameObject _colorParent;

    [SerializeField] Button _deleteButton;

    private GameObject _selectType;

    private bool _isActive = true;

    private bool _isSelect = true;

    private Outline _outline;

    private CarTypeSO _currentTypeSO;

    private int _currentColorNumber;

    private int _currentSO;

    private CarHolder _selectedDelet;

    private void Start()
    {
        LoadGame();
        _countText.text = $"{_carsHolder.Count}";
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        KeeperSaver keeperSaver = new KeeperSaver();

        List<Keeper> keepers = new List<Keeper>();

        foreach (var holder in _carsHolder)
        {
            Keeper keeper = new Keeper();

            keeper.Name = holder.Car.Name;
            keeper.Gasoline = holder.Gasoline.Gasl;
            keeper.Doors = holder.Doors.Door;
            keeper.Power = holder.Powers.Power;
            keeper.SO = holder.SO;
            keeper.ColorNumber = holder.ColorNumber;

            keepers.Add(keeper);
        }

        keeperSaver.Keepers = keepers.ToArray();

        string json = JsonUtility.ToJson(keeperSaver);

        File.WriteAllText("save.json",json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText("save.json");

        KeeperSaver keeperSaver = JsonUtility.FromJson<KeeperSaver>(json);

        foreach (var kepeer in keeperSaver.Keepers)
        {
            CreateCar(kepeer.Name, kepeer.Gasoline, kepeer.Doors, kepeer.Power, _carTypesSO[kepeer.SO].Sprites[kepeer.ColorNumber], kepeer.SO, kepeer.ColorNumber);
        }
    }

    public void SetOutline(Outline outline)
    {
        if (_isActive || outline != _outline)
        {
            foreach (var ol in _colorParent.GetComponentsInChildren<Outline>())
            {
                ol.enabled = false;
            }

            _outline = outline;
            outline.enabled = true;
            _isActive = false;
        }

        else if(_isActive == false)
        {
            outline.enabled = false;

            _isActive = true;
        }
    }

    public void SelectedTypeCar(GameObject select)
    {
        _selectType?.SetActive(false);

        if (_isSelect || _selectType != select)
        {
            _selectType = select;
            select.SetActive(true);

            _isSelect = false;
        }

        else if (_isSelect == false)
        {
            select.SetActive(false);

            _isSelect = true;
        }
    }

    public void SetTypeSO(CarTypeSO carTypeSO)
    {
        _currentTypeSO = carTypeSO;
    }

    public void ColorChange(int number)
    {
        _currentColorNumber = number;
    }

    private void CreateCar(string name,int gasolines,int door, int power, Sprite texture,int so, int colorNumber)
    {
        Car car = new Car(name);
        Gasoline gasoline = new Gasoline(gasolines);
        Doors doors = new Doors(door);
        Powers powers = new Powers(power);

        var carHolder = Instantiate(_carHolderPrefab, _context);
        carHolder.Init(car, gasoline,doors,powers,texture,this,so,colorNumber);

        _carsHolder.Add(carHolder);

        _cars.Add(car);
    }

    private void ClearCars(bool isCarsObejct = true)
    {
        foreach (var carHolder in _carsHolder) Destroy(carHolder.gameObject);

        _carsHolder = new List<CarHolder>();

        if (isCarsObejct) _cars = new List<Car>();

    }

    public void ListName()
    {

        if (String.IsNullOrEmpty(_inputName.text) == true) return;
        if (int.TryParse(_inputGasoline.text,out int gasoline) == false) return;
        if (int.TryParse(_inputDoors.text, out int doors) == false) return;
        if (int.TryParse(_inputPower.text,out int power) == false) return;
        if (int.TryParse(_inputCount.text,out int count) == false) return;
        if (_currentTypeSO == null || _currentTypeSO.Sprites == null || (_currentTypeSO.Sprites.Count - 1) < _currentColorNumber) return;

        string name = _inputName.text;

        for (int i = 0; i < count; i++)
        {
            CreateCar(name, gasoline, doors, power,_currentTypeSO.Sprites[_currentColorNumber],_carTypesSO.IndexOf(_currentTypeSO),_currentColorNumber);
        }

        _countText.text = $"{_carsHolder.Count}";

        ClearValues();
    }


    public void ClearList()
    {
        ClearCars();
        _countText.text = $"{_carsHolder.Count}";
        ClearValues();

        _deleteButton.interactable = false;

        _selectedDelet = null;
    }



    private void ClearValues()
    {
        _inputName.text = "";
        _inputGasoline.text = "";
        _inputDoors.text = "";
        _inputPower.text = "";
        _inputCount.text = "";

        _isActive = false;

        _outline.enabled = false;
        _isSelect = false;

        _selectType.SetActive(false);
        _selectType = null;

        _currentColorNumber = 0;

        _currentTypeSO = null;
    }

    public void Delete()
    {
        if (_selectedDelet == null) return;

        _carsHolder.Remove(_selectedDelet);
        _countText.text = $"{_carsHolder.Count}";

        Destroy(_selectedDelet.gameObject);

        _selectedDelet = null;

        _deleteButton.interactable = false;
    }

    public void SelectObject(CarHolder carHolder)
    {
        if (_selectedDelet != null)
        {
            _selectedDelet.Obvodka.SetActive(false);
        }

        if (_selectedDelet == carHolder)
        {
            _selectedDelet = null;
            _deleteButton.interactable = false;
            return;
        }
        carHolder.Obvodka.SetActive(true);

        _selectedDelet = carHolder;

        _deleteButton.interactable = true;
    }

    public void Searh()
    {
        List<string> namesCars = new List<string>();

        foreach (var car in _cars) namesCars.Add(car.Name);

        ClearCars();

        var names = GetStringToLenght(0, 5, namesCars);

        //foreach (var name in names) CreateCar(name);
    }

    private List<string> GetStringToLenght(int lenghtMin,int lenghtMax,List<string> listString)
    {
        var listTemp = new List<string>();

        for(int i = 0; i < listString.Count; i++)
        {
            if(listString[i].Length >= lenghtMin && listString[i].Length <= lenghtMax)
            {
                listTemp.Add(listString[i]);
            }
        }

        return listTemp;
    }

    public void InversionList()
    {
        ClearCars(false);

        _cars.Reverse();

        //foreach (var car in _cars) CreateCar(car);
    }

    public class Gasoline
    {
        public int Gasl { get; private set; }

        public Gasoline(int gasoline)
        {
            Gasl = gasoline;
        }

    }

    public class Doors
    {
        public int Door { get; private set; }

        public Doors(int doors)
        {
            Door = doors;
        }

    }

    public class Powers
    {
        public int Power { get; private set; }

        public Powers(int powers)
        {
            Power = powers;
        }

    }
}