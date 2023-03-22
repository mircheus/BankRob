using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DataManagerDISABLED : MonoBehaviour
{
    [SerializeField] private TMP_Text _inputField;
    
    private PlayerData _playerStats = new PlayerData();
    private JsonDataServiceEXAMPLE _dataServiceExample = new JsonDataServiceEXAMPLE();
    private bool _encryptionEnabled = false;

    // public event UnityAction DataUpdated;
        
    public PlayerData PlayerStats => _playerStats;

    private void Start()
    {
        Debug.Log(_playerStats == null);
    }

    public void SerializeJson()
    {
        if (_dataServiceExample.SaveData("/TestData.json", _playerStats, _encryptionEnabled))
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
        PlayerData playerData = _dataServiceExample.LoadData<PlayerData>("/TestData.json", _encryptionEnabled);
        _playerStats = playerData;
        _inputField.text = JsonConvert.SerializeObject(playerData, Formatting.Indented);
        // DataUpdated?.Invoke();
    }
 
    public void IncrementMoney()
    {
        _playerStats.IncrementMoney();
        // DataUpdated?.Invoke();
    }

    public void IncrementKeys()
    {
        _playerStats.IncrementKeys();
        // DataUpdated?.Invoke();
    }
}