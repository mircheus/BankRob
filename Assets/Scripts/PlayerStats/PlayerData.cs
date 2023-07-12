using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;
using Unity.VisualScripting;

public class PlayerData : MonoBehaviour
{
    private const string LeaderboardName = "Money";
    private const string IsAuthorizedPlayerPref = "isAuthorized";
    
    [SerializeField] private Robbery _robbery;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private BarriersProgression barriersProgression;
    [SerializeField] private EconomicProgression _economicProgression;
    [SerializeField] private AdPlayer _adPlayer;
    [SerializeField] private int _adRewardAmount;
    [SerializeField] private Grid _grid;
    [SerializeField] private RobbersSaver _robbersSaver;
    [SerializeField] private LeaderboardLoader _leaderboardLoader;

    [Header("DEBUG data values")]
    [SerializeField] private int _keysAmount;
    [SerializeField] private int _moneyAmount;
    [SerializeField] private int _completedLevelsCounter;
    [SerializeField] private int _floorsAmountFromPreviousLevel;
    [SerializeField] private int _allMoneyCounter;
    [SerializeField] private int[] _robbersToSave;
    [SerializeField] private int _achievedLevels;
    [SerializeField] private bool _isTryAgain;
    [SerializeField] private bool _isAuthorized;
    
    // [SerializeField] private int _currentPrice;
    // [SerializeField] private int _currentReward;

    public event UnityAction DataUpdated;
    public event UnityAction DataLoaded;
    public event UnityAction<int> NewLevelAchieved;
    
    public int KeysAmount => _keysAmount;
    public int MoneyAmount => _moneyAmount;
    public int CompletedLevelsCounter => _completedLevelsCounter;
    public int FloorsAmountFromPreviousLevel => _floorsAmountFromPreviousLevel;
    public int[] RobbersToSave => _robbersToSave;
    public bool IsTryAgain => _isTryAgain;
    public bool IsAuthorized => _isAuthorized;

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

    public void AuthorizeBy(Authorization authorization)
    {
        _isAuthorized = true;
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
            Leaderboard.SetScore(_leaderboardLoader.LeaderboardName, _allMoneyCounter);
        }
#endif

        // _aliveRobbers = _robbery.CountAliveRobbers();
        _robbersToSave = _robbersSaver.RobbersToSave;
        _completedLevelsCounter++;
        _isTryAgain = false;
        PlayerPrefs.SetInt(IsAuthorizedPlayerPref, Convert.ToInt32(_isAuthorized));
        SaveCurrentPlayerStats();
    }

    private void OnBankNotRobbed()
    {
        // _aliveRobbers = null;
        _robbersToSave = _robbersSaver.RobbersToSave;
        _isTryAgain = true;
        SaveCurrentPlayerStats();
    }

    private void OnVideoAdPlayed()
    {
        _moneyAmount += _robbery.MoneyRewardAmount;
        Debug.Log($"moneyAmount = {_moneyAmount}");
        Debug.Log($"robbery.MoneyRewardAmount = {_robbery.MoneyRewardAmount}");
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
            SetPlayerStats(loadedData.Money, loadedData.AllMoneyCounter, loadedData.CompletedLevelsCounter, loadedData.PreviousLevelFloorsAmount, loadedData.AliveRobbers, loadedData.AchievedLevels, loadedData.CurrentPrice, loadedData.CurrentReward, loadedData.IsTryAgain);
            _isAuthorized = Convert.ToBoolean(PlayerPrefs.GetInt(IsAuthorizedPlayerPref));
        }
        else
        {
            SetPlayerStats(_economicProgression.StarterMoneyAmount, 0, 0, barriersProgression.FirstLevelFloorsAmount, null, 0, _economicProgression.StartPrice, _economicProgression.StartReward, false);
            _isAuthorized = false;
        } 
    }
    
    private void ResetPlayerStats()
    {
        SavePlayerStats(_economicProgression.StarterMoneyAmount, 0,0, 0, barriersProgression.FirstLevelFloorsAmount, null, 0, _economicProgression.StartPrice, _economicProgression.StartReward, false, false);
    }

    private void SaveCurrentPlayerStats()
    {
        SavePlayerStats(_moneyAmount, _allMoneyCounter, _keysAmount, _completedLevelsCounter, barriersProgression.FloorsQuantity, _robbersToSave, _achievedLevels, _economicProgression.CurrentPrice, _economicProgression.RewardToNextLevel, _isTryAgain, _isAuthorized);
    }

    private void SetPlayerStats(int moneyAmount, int allMoneyCounter, int completedLevelsAmount, int floorsAmount, int[] aliveRobbers, int achievedLevels, int currentPrice, int currentReward, bool isTryAgain)
    {
        _moneyAmount = moneyAmount;
        _allMoneyCounter = allMoneyCounter;
        _completedLevelsCounter = completedLevelsAmount;
        _floorsAmountFromPreviousLevel = floorsAmount;
        _robbersToSave = aliveRobbers;
        _achievedLevels = achievedLevels;
        _economicProgression.SetCurrentValues(currentPrice, currentReward);
        _isTryAgain = isTryAgain;
        // _isAuthorized = isAuthorized;
        DataUpdated?.Invoke();
    }

    private void SavePlayerStats(int moneyAmount, int allMoneyCounter,int keysAmount, int completedLevelsCounter, int currentLevelFloorsAmount, int[] aliveRobbers, int achievedLevels,int currentPrice, int currentReward, bool isTryAgain, bool isAuthorized)
    {
        // Debug.Log($"Convert to int {Convert.ToInt32(_isAuthorized)}");
        
        // PlayerPrefs.SetInt("isAuthorized", Convert.ToInt32(_isAuthorized));
        Data dataToSave = new Data(moneyAmount, allMoneyCounter, keysAmount, completedLevelsCounter, currentLevelFloorsAmount, aliveRobbers, achievedLevels, currentPrice, currentReward, isTryAgain, isAuthorized);
        _dataManager.SaveData(dataToSave);
    }
}
