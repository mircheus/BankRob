using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyIndicator : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TMP_Text _moneyIndicator;
    
    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
        _playerData.DataUpdated += OnDataUpdated;
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
        _playerData.DataUpdated -= OnDataUpdated;
    }

    private void OnDataLoaded()
    {
        SetMoneyAmount();
    }

    private void OnDataUpdated()
    {
        SetMoneyAmount();
    }

    private void SetMoneyAmount()
    {
        _moneyIndicator.text = _playerData.MoneyAmount.ToString();
    }
}
