using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private PlayerData _playerStats = new PlayerData();
    private JsonDataService _dataService = new JsonDataService();
    private bool _encryptionEnabled = false;

    public PlayerData PlayerStats => _playerStats;

    private void Awake()
    {
        
    }

    public void SerializeJson()
    {
        if (_dataService.SaveData("/TestData.json", _playerStats, _encryptionEnabled))
        {
            Debug.Log("File successfully saved!");
        }
        else
        {
            Debug.LogError($"Could not save the file!");
        }
    }

    public void DeserializeJson()
    {
        PlayerData playerData = _dataService.LoadData<PlayerData>("/TestData.json", _encryptionEnabled);
        Debug.Log(JsonConvert.SerializeObject(playerData));
        Debug.Log($"Money: {playerData.Money}");
        Debug.Log($"Keys: {playerData.Keys}");
    }

    public void IncrementMoney()
    {
        _playerStats.IncrementMoney();
    }

    public void IncrementKeys()
    {
        _playerStats.IncrementKeys();
    }
}