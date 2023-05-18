using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Robbery _robbery;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private Progression _progression;

    [Header("DEBUG data values")]
    [SerializeField] private int _keysAmount;
    [SerializeField] private int _moneyAmount;
    [SerializeField] private int _completedLevelsCounter;
    [SerializeField] private int _floorsAmountFromPreviousLevel;
    [SerializeField] private int _allMoneyCounter;

    public event UnityAction DataUpdated;
    public event UnityAction DataLoaded;
    
    public int KeysAmount => _keysAmount;
    public int MoneyAmount => _moneyAmount;
    public int CompletedLevelsCounter => _completedLevelsCounter;
    public int FloorsAmountFromPreviousLevel => _floorsAmountFromPreviousLevel;

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
        SavePlayerStats(_progression.InitMoney, 0, 0, _progression.FirstLevelFloorsAmount);
        DataUpdated?.Invoke();
    }

    private void OnKeyCollected()
    {
        _keysAmount++;
        DataUpdated?.Invoke(); // DEBUG REFLECTOR
    }

    private void OnPreparing()
    {
        LoadDataFromFile();
        DataLoaded?.Invoke();
        _keysAmount = 0;
    }

    private void OnBankRobbed()
    {
        _moneyAmount += _robbery.MoneyRewardAmount;
        _allMoneyCounter += _robbery.MoneyRewardAmount;

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.SetScore("Money", _allMoneyCounter); // Magic STRING
        }
#endif
        
        SavePlayerStats(_moneyAmount, _keysAmount, _completedLevelsCounter, _progression.FloorsQuantity);
    }

    private void LoadDataFromFile()
    {
        if (_dataManager.IsLoadDataPersists())
        {
            Data loadedData = _dataManager.LoadData();
            SetPlayerStats(loadedData.Money, loadedData.Keys, loadedData.CompletedLevelsCounter, loadedData.PreviousLevelFloorsAmount);
        }
        else
        {
            SetPlayerStats(_progression.InitMoney, 0, 0, _progression.FirstLevelFloorsAmount);
        } 
    }

    private void SetPlayerStats(int moneyAmount, int keysAmount, int completedLevelsAmount, int floorsAmount)
    {
        _moneyAmount = moneyAmount;
        _keysAmount = keysAmount;
        _completedLevelsCounter = completedLevelsAmount;
        _floorsAmountFromPreviousLevel = floorsAmount;
        DataUpdated?.Invoke();
    }

    private void SavePlayerStats(int moneyAmount, int keysAmount, int completedLevelsCounter, int currentLevelFloorsAmount)
    {
        // completedLevelsCounter++;

        Data dataToSave = new Data(moneyAmount, keysAmount, ++completedLevelsCounter, currentLevelFloorsAmount); 
        _dataManager.SaveData(dataToSave);
    }
}
