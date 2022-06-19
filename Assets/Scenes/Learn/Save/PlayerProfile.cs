using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [SerializeField] private CarHolder _carHolder;

    private const string saveKey = "mainSave";

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerProfile>(saveKey);

        //_carHolder.SetVa
    }

    private void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SaveData.PlayerProfile GetSaveSnapshot()
    {
        var data = new SaveData.PlayerProfile()
        {
            CarHolder = _carHolder
        };

        return data;
    }
}
