using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;

public class PlayerData : MonoBehaviour
{
    private const string LeaderboardName = "Money";
    
    [SerializeField] private Robbery _robbery;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private BarriersProgression barriersProgression;
    [SerializeField] private EconomicProgression _economicProgression;
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
    // [SerializeField] private int _currentPrice;
    // [SerializeField] private int _currentReward;

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
        DataUpdated?.Invoke();
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
            Leaderboard.SetScore(LeaderboardName, _allMoneyCounter);
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
        }
    }
    
    private void LoadDataFromFile()
    {
        if (_dataManager.IsLoadDataPersists())
        {
            Data loadedData = _dataManager.LoadData();
            SetPlayerStats(loadedData.Money, loadedData.Keys, loadedData.CompletedLevelsCounter, loadedData.PreviousLevelFloorsAmount, loadedData.AliveRobbers, loadedData.AchievedLevels, loadedData.CurrentPrice, loadedData.CurrentReward);
        }
        else
        {
            SetPlayerStats(barriersProgression.InitMoney, 0, 0, barriersProgression.FirstLevelFloorsAmount, null, 0, _economicProgression.StartPrice, _economicProgression.StartReward);
        } 
    }
    
    private void ResetPlayerStats()
    {
        SavePlayerStats(_economicProgression.StarterMoneyAmount, 0,0, 0, barriersProgression.FirstLevelFloorsAmount, null, 0, _economicProgression.StartPrice, _economicProgression.StartReward);
    }

    private void SaveCurrentPlayerStats()
    {
        SavePlayerStats(_moneyAmount, _allMoneyCounter, _keysAmount, _completedLevelsCounter, barriersProgression.FloorsQuantity, _aliveRobbers, _achievedLevels, _economicProgression.CurrentPrice, _economicProgression.RewardToNextLevel);
    }

    private void SetPlayerStats(int moneyAmount, int keysAmount, int completedLevelsAmount, int floorsAmount, int[] aliveRobbers, int achievedLevels, int currentPrice, int currentReward)
    {
        _moneyAmount = moneyAmount;
        _keysAmount = keysAmount;
        _completedLevelsCounter = completedLevelsAmount;
        _floorsAmountFromPreviousLevel = floorsAmount;
        _aliveRobbers = aliveRobbers;
        _achievedLevels = achievedLevels;
        _economicProgression.SetCurrentValues(currentPrice, currentReward);
        DataUpdated?.Invoke();
    }

    private void SavePlayerStats(int moneyAmount, int allMoneyCounter,int keysAmount, int completedLevelsCounter, int currentLevelFloorsAmount, int[] aliveRobbers, int achievedLevels,int currentPrice,int currentReward)
    {
        Data dataToSave = new Data(moneyAmount, allMoneyCounter, keysAmount, ++completedLevelsCounter, currentLevelFloorsAmount, aliveRobbers, achievedLevels, currentPrice, currentReward); 
        _dataManager.SaveData(dataToSave);
    }
}
