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
    
    private PlayerData _stats = new PlayerData();
    private JsonDataServiceEXAMPLE _dataServiceExample = new JsonDataServiceEXAMPLE();
    private bool _encryptionEnabled = false;

    // public event UnityAction DataUpdated;
        
    public PlayerData Stats => _stats;

    private void Start()
    {
        Debug.Log(_stats == null);
    }

    public void SerializeJson()
    {
        if (_dataServiceExample.SaveData("/TestData.json", _stats, _encryptionEnabled))
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
        PlayerData data = _dataServiceExample.LoadData<PlayerData>("/TestData.json", _encryptionEnabled);
        _stats = data;
        _inputField.text = JsonConvert.SerializeObject(data, Formatting.Indented);
        // DataUpdated?.Invoke();
    }
 
    public void IncrementMoney()
    {
        // _playerStats.IncrementMoney();
        // DataUpdated?.Invoke();
    }

    public void IncrementKeys()
    {
        // _playerStats.IncrementKeys();
        // DataUpdated?.Invoke();
    }
}