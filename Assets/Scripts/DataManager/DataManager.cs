using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using File = System.IO.File;

public class DataManager : MonoBehaviour
{
    private JsonDataService _jsonDataService = new JsonDataService();
    // private Data _currentData = new Data(0, 0);
    private string _relativePath = "/progression_testing.json";
    private bool _isEncrypted = false;
    private string _loadedData;
    
    // public event UnityAction DataUpdated;
    
    // public Data CurrentData => _currentData;

    public void SaveDataToYandex(Data dataToSave)
    {
        PlayerAccount.SetPlayerData(JsonConvert.SerializeObject(dataToSave));
    }

    public void LoadDataFromYandex()
    {
        string loadedData;
        PlayerAccount.GetPlayerData((data) => loadedData = data);
    }
    
    public void SaveData(Data dataToSave)
    {
        // Data dataToSave = new Data(_currentData.Money, _currentData.Keys);

        if (_jsonDataService.SaveData(_relativePath, dataToSave, _isEncrypted))
        {
            // Debug.Log("Success");
        }
        else
        {
            // Debug.Log("ERROR");
        }
    }

    public bool IsLoadDataPersists()
    {
        string path = Application.persistentDataPath + _relativePath;

        return File.Exists(path);
    }

    public Data LoadData()
    {
         return _jsonDataService.LoadData<Data>(_relativePath, _isEncrypted);
    }
} 
