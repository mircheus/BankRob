using System;
using TMPro;
using UnityEngine;

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
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private BarriersProgression _barriersProgression;
    
    private void OnEnable()
    {
        _playerData.DataUpdated += OnPlayerDataUpdated;
        _playerData.DataLoaded += RefreshDataReflection;
    }

    private void OnDisable()
    {
        _playerData.DataUpdated -= OnPlayerDataUpdated;
        _playerData.DataLoaded -= RefreshDataReflection;
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
