using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private JsonDataService _jsonDataService = new JsonDataService();
    private Data _currentData = new Data(0, 0);
    private string _relativePath = "/savegame.json";
    private bool _isEncrypted = false;
    
    public event UnityAction DataUpdated;
    
    public Data CurrentData => _currentData;

    public void SaveData()
    {
        Data dataToSave = new Data(_currentData.Money, _currentData.Keys);

        if (_jsonDataService.SaveData(_relativePath, dataToSave, _isEncrypted))
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
        _currentData = _jsonDataService.LoadData<Data>(_relativePath, _isEncrypted);
        DataUpdated?.Invoke();
    }

    public void IncrementMoney()
    {
        _currentData.IncrementMoney();
        DataUpdated?.Invoke();
    }

    public void IncrementKeys()
    {
        _currentData.IncrementKeys(); 
        DataUpdated?.Invoke();
    }
} 
