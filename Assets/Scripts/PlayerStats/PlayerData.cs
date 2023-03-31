using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Robbery _robbery;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private DataManager _dataManager;

    [Header("DEBUG data values")]
    [SerializeField] private int _keysAmount;
    [SerializeField] private int _moneyAmount;
    [SerializeField] private int _completedLevelsCounter;

    public event UnityAction DataUpdated;
    
    public int KeysAmount => _keysAmount;
    public int MoneyAmount => _moneyAmount;
    public int CompletedLevelsCounter => _completedLevelsCounter;

    private void OnEnable()
    {
        _robbery.BankRobbed += OnBankRobbed;
        _preparing.PreparingStarted += OnPreparing;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
        _preparing.PreparingStarted -= OnPreparing;
    }

    public void PayForRobber(int money)
    {
        _moneyAmount -= money;
    }
    
    public void SubscribeToKeyCollector(Slot slot)
    {
        slot.GetComponentInChildren<KeyCollector>().KeyCollected += OnKeyCollected;
    }

    public void UnsubscribeFromKeyCollector(Slot slot)
    {
        slot.GetComponentInChildren<KeyCollector>().KeyCollected -= OnKeyCollected;
    }

    private void OnKeyCollected()
    {
        _keysAmount++;
        DataUpdated?.Invoke(); // DEBUG REFLECTOR
    }

    private void OnPreparing()
    {
        LoadDataFromFile();
    }

    private void OnBankRobbed()
    {
        _moneyAmount += _robbery.MoneyRewardAmount;
        SavePlayerStats();
    }

    private void LoadDataFromFile()
    {
        Data loadedData = _dataManager.LoadData();
        _moneyAmount = loadedData.Money;
        _keysAmount = loadedData.Keys;
        _completedLevelsCounter = loadedData.CompletedLevelsCounter;
        DataUpdated?.Invoke();
    }

    private void SavePlayerStats()
    {
        Data dataToSave = new Data(_moneyAmount, _keysAmount, ++_completedLevelsCounter); 
        _dataManager.SaveData(dataToSave);
    }
}
