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

    [Header("DEBUG")]
    [SerializeField] private int _keysAmount;
    [SerializeField] private int _moneyAmount;

    public event UnityAction DataUpdated;
    
    public int KeysAmount => _keysAmount;
    public int MoneyAmount => _moneyAmount;

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
        _moneyAmount += 6;
        DataUpdated?.Invoke();
    }

    private void OnPreparing()
    {
        Data loadedData = _dataManager.LoadData();
        _moneyAmount = loadedData.Money;
        _keysAmount = loadedData.Keys;
        DataUpdated?.Invoke();
    }

    private void OnBankRobbed()
    {
        Data dataToSave = new Data(_moneyAmount, _keysAmount);
        _dataManager.SaveData(dataToSave);
    }
}
