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
    private string _relativePath = "/progression_testing.json";
    private string _loadedData;

    public void SaveData(Data dataToSave)
    {
        if (_jsonDataService.SaveData(_relativePath, dataToSave))
        {
            Debug.Log("_jsonDataService.SaveData: Success");
        }
        else
        {
            Debug.Log("_jsonDataService.SaveData: ERROR");
        }
    }

    public bool IsLoadDataPersists()
    {
        string path = Application.persistentDataPath + _relativePath;

        return File.Exists(path);
    }

    public Data LoadData()
    {
         return _jsonDataService.LoadData<Data>(_relativePath);
    }
} 
