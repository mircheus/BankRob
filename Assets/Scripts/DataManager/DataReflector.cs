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
    [SerializeField] private TMP_Text _trapsQuantity;

    [Header("Data Sources")]
    // [SerializeField] private DataManager _dataManager;
    [SerializeField] private PlayerData _playerData;
    // [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private BarriersProgression _barriersProgression;
    
    private void OnEnable()
    {
        // _dataManager.DataUpdated += OnDataUpdated;
        _playerData.DataUpdated += OnPlayerDataUpdated;
        _playerData.DataLoaded += RefreshDataReflection;
    }

    private void OnDisable()
    {
        // _dataManager.DataUpdated -= OnDataUpdated;
        _playerData.DataUpdated -= OnPlayerDataUpdated;
        _playerData.DataLoaded -= RefreshDataReflection;
    }

    private void Start()
    {
        // RefreshDataReflection();
    }

    private void OnPlayerDataUpdated()
    {
        RefreshDataReflection();
    }

    private void RefreshDataReflection()
    {
        _keysField.text = Convert.ToString(_playerData.KeysAmount);
        _moneyField.text = Convert.ToString(_playerData.MoneyAmount);
        _levelCounterField.text = Convert.ToString(_playerData.CompletedLevelsCounter);
        _floors.text = Convert.ToString(_barriersProgression.FloorsQuantity - 1); // active floors without vaults
        _obstaclesLevel.text = Convert.ToString(_barriersProgression.ObstaclesLevel);
        _obstaclesQuantity.text = Convert.ToString(_barriersProgression.ObstaclesQuantity);
        _trapsQuantity.text = Convert.ToString(_barriersProgression.TrapsQuantity);
    }
}
