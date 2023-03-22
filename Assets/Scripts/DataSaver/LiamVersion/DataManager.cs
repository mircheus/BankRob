using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _inputField;
    
    private PlayerData _playerStats = new PlayerData();
    private JsonDataService _dataService = new JsonDataService();
    private bool _encryptionEnabled = false;

    public event UnityAction DataUpdated;
        
    public PlayerData PlayerStats => _playerStats;

    private void Start()
    {
        Debug.Log(_playerStats == null);
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
        _playerStats = playerData;
        _inputField.text = JsonConvert.SerializeObject(playerData, Formatting.Indented);
        DataUpdated?.Invoke();
    }
 
    public void IncrementMoney()
    {
        _playerStats.IncrementMoney();
        DataUpdated?.Invoke();
    }

    public void IncrementKeys()
    {
        _playerStats.IncrementKeys();
        DataUpdated?.Invoke();
    }
}