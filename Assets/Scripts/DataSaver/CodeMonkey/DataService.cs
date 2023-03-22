using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataService : MonoBehaviour
{
    private SaveManager _saveManager = new SaveManager();
    private Data _data = new Data();

    public Data Data => _data;

    private void Start()
    {
        _data.Money = 0;
        _data.Keys = 0;
    }

    public void SaveData()
    {
        Data dataToSave = new Data()
        {
            Money = _data.Money,
            Keys = _data.Keys,
        };

        if (_saveManager.Save(dataToSave))
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    public void LoadData()
    {
        Data loadedData = _saveManager.Load<Data>();
        _data = loadedData;
    }

    public void IncrementMoney()
    {
        _data.IncrementMoney();
    }

    public void IncrementKeys()
    {
        _data.IncrementKeys(); 
    }
} 
