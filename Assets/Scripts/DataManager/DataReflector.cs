using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

public class DataReflector : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyField;
    [SerializeField] private TMP_Text _keysField;
    [SerializeField] private TMP_Text _levelCounterField;
    [SerializeField] private TMP_Text _floors;
    [SerializeField] private TMP_Text _obstaclesLevel;
    [SerializeField] private TMP_Text _obstaclesQuantity;

    [Header("Data Sources")]
    // [SerializeField] private DataManager _dataManager;
    [SerializeField] private PlayerData _playerData;
    // [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private BarriersProgression barriersProgression;
    
    private void OnEnable()
    {
        // _dataManager.DataUpdated += OnDataUpdated;
        _playerData.DataUpdated += OnPlayerDataUpdated;
    }

    private void OnDisable()
    {
        // _dataManager.DataUpdated -= OnDataUpdated;
        _playerData.DataUpdated -= OnPlayerDataUpdated;
    }

    private void Start()
    {
        _moneyField.text = Convert.ToString(-1);
        _keysField.text = Convert.ToString(-1);
        _levelCounterField.text = Convert.ToString(-1);
    }
    
    private void OnPlayerDataUpdated()
    {
        _keysField.text = Convert.ToString(_playerData.KeysAmount);
        _moneyField.text = Convert.ToString(_playerData.MoneyAmount);
        _levelCounterField.text = Convert.ToString(_playerData.CompletedLevelsCounter);
        _floors.text = Convert.ToString(barriersProgression.FloorsQuantity - 1); // active floors without vaults
        _obstaclesLevel.text = Convert.ToString(barriersProgression.ObstaclesLevel);
        _obstaclesQuantity.text = Convert.ToString(barriersProgression.ObstaclesQuantity);
    }
}
