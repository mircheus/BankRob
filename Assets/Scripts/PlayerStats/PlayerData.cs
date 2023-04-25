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
    public event UnityAction DataLoaded;
    
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
        DataUpdated?.Invoke();
    }
    
    public void SubscribeToKeyCollector(Robber robber)
    {
        robber.GetComponent<KeyCollector>().KeyCollected += OnKeyCollected;
    }

    public void UnsubscribeFromKeyCollector(Robber robber) 
    {
        robber.GetComponent<KeyCollector>().KeyCollected -= OnKeyCollected;
    }

    public void ResetPlayerData()
    {
        _keysAmount = 0;
        _moneyAmount = 100;
        _completedLevelsCounter = 0;
        SavePlayerStats();
        DataUpdated?.Invoke();
    }

    private void OnKeyCollected()
    {
        _keysAmount++;
        DataUpdated?.Invoke(); // DEBUG REFLECTOR
    }

    private void OnPreparing()
    {
        Debug.Log("OnPreparing invoked");
        LoadDataFromFile();
        DataLoaded?.Invoke();
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
        _completedLevelsCounter++;
        Data dataToSave = new Data(_moneyAmount, _keysAmount, _completedLevelsCounter); 
        _dataManager.SaveData(dataToSave);
    }
}
