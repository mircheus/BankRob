using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataReflector : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyField;
    [SerializeField] private TMP_Text _keysField;
    [SerializeField] private DataManager _dataManager;

    [SerializeField] private PlayerData _playerData;
    
    private void OnEnable()
    {
        // _dataManager.DataUpdated += OnDataUpdated;
        _playerData.DataUpdated += OnDataUpdated;
    }

    private void OnDisable()
    {
        // _dataManager.DataUpdated -= OnDataUpdated;
        _playerData.DataUpdated -= OnDataUpdated;
    }

    private void Start()
    {
        _moneyField.text = Convert.ToString(-1);
        _keysField.text = Convert.ToString(-1);
    }
    
    private void OnDataUpdated()
    {
        // _moneyField.text = Convert.ToString(_dataManager.CurrentData.Money);
        // _keysField.text = Convert.ToString(_dataManager.CurrentData.Keys);
        _keysField.text = Convert.ToString(_playerData.KeysAmount);
    }
}
