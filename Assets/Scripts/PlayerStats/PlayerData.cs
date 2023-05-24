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
    [SerializeField] private AdPlayer _adPlayer;
    [SerializeField] private int _adRewardAmount;
    [SerializeField] private Grid _grid;

    [Header("DEBUG data values")]
    [SerializeField] private int _keysAmount;
    [SerializeField] private int _moneyAmount;
    [SerializeField] private int _completedLevelsCounter;
    [SerializeField] private int _floorsAmountFromPreviousLevel;
    [SerializeField] private int _allMoneyCounter;
    [SerializeField] private int[] _aliveRobbers;
    [SerializeField] private int _achievedLevels;

    public event UnityAction DataUpdated;
    public event UnityAction DataLoaded;
    public event UnityAction<int> NewLevelAchieved;
    
    public int KeysAmount => _keysAmount;
    public int MoneyAmount => _moneyAmount;
    public int CompletedLevelsCounter => _completedLevelsCounter;
    public int FloorsAmountFromPreviousLevel => _floorsAmountFromPreviousLevel;
    public int[] AliveRobbers => _aliveRobbers;

    private void OnEnable()
    {
        _robbery.BankRobbed += OnBankRobbed;
        _robbery.BankNotRobbed += OnBankNotRobbed;
        _preparing.PreparingStarted += OnPreparing;
        _adPlayer.VideoAdPlayed += OnVideoAdPlayed;
        _grid.RobbersCombined += OnRobbersCombined;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
        _robbery.BankNotRobbed -= OnBankNotRobbed;
        _preparing.PreparingStarted -= OnPreparing;
        _adPlayer.VideoAdPlayed -= OnVideoAdPlayed;
        _grid.RobbersCombined -= OnRobbersCombined;
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
        ResetPlayerStats();
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

        _aliveRobbers = _robbery.CountAliveRobbers();
        SaveCurrentPlayerStats();
    }

    private void OnBankNotRobbed()
    {
        _aliveRobbers = null;
        SaveCurrentPlayerStats();
    }

    private void OnVideoAdPlayed()
    {
        _moneyAmount += _adRewardAmount;
        SaveCurrentPlayerStats();
    }

    private void OnRobbersCombined(int level)
    {
        if (level > _achievedLevels)
        {
            _achievedLevels = level;
            NewLevelAchieved?.Invoke(level);
            Debug.Log($"Achieved {_achievedLevels} level!");
        }
    }
    
    private void LoadDataFromFile()
    {
        if (_dataManager.IsLoadDataPersists())
        {
            Data loadedData = _dataManager.LoadData();
            SetPlayerStats(loadedData.Money, loadedData.Keys, loadedData.CompletedLevelsCounter, loadedData.PreviousLevelFloorsAmount, loadedData.AliveRobbers, loadedData.AchievedLevels);
        }
        else
        {
            SetPlayerStats(_progression.InitMoney, 0, 0, _progression.FirstLevelFloorsAmount, null, 1);
        } 
    }
    
    private void ResetPlayerStats()
    {
        SavePlayerStats(_progression.InitMoney, 0,0, 0, _progression.FirstLevelFloorsAmount, null, 0);
    }

    private void SaveCurrentPlayerStats()
    {
        SavePlayerStats(_moneyAmount, _allMoneyCounter, _keysAmount, _completedLevelsCounter, _progression.FloorsQuantity, _aliveRobbers, _achievedLevels);
    }

    private void SetPlayerStats(int moneyAmount, int keysAmount, int completedLevelsAmount, int floorsAmount, int[] aliveRobbers, int achievedLevels)
    {
        _moneyAmount = moneyAmount;
        _keysAmount = keysAmount;
        _completedLevelsCounter = completedLevelsAmount;
        _floorsAmountFromPreviousLevel = floorsAmount;
        _aliveRobbers = aliveRobbers;
        _achievedLevels = achievedLevels;
        DataUpdated?.Invoke();
    }

    private void SavePlayerStats(int moneyAmount, int allMoneyCounter,int keysAmount, int completedLevelsCounter, int currentLevelFloorsAmount, int[] aliveRobbers, int achievedLevels)
    {
        Data dataToSave = new Data(moneyAmount, allMoneyCounter, keysAmount, ++completedLevelsCounter, currentLevelFloorsAmount, aliveRobbers, achievedLevels); 
        _dataManager.SaveData(dataToSave);
    }
}
