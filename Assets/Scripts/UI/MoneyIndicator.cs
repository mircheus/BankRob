using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyIndicator : MonoBehaviour
{   
    [Header("Prepare stage")]
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TMP_Text _moneyIndicator;

    [Header("Win stage")] 
    [SerializeField] private Robbery _robbery;
    [SerializeField] private TMP_Text _rewardMoney;

    [Header("Events sources")] 
    [SerializeField] private AdPlayer _adPlayer;
    [SerializeField] private EconomicProgression _economicProgression;

    private const string PlusSign = "+";
    
    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
        _playerData.DataUpdated += OnDataUpdated;
        _robbery.BankRobbed += OnBankRobbed;
        _adPlayer.VideoAdPlayed += OnVideoAdPlayed;
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
        _playerData.DataUpdated -= OnDataUpdated;
        _robbery.BankRobbed -= OnBankRobbed;
        _adPlayer.VideoAdPlayed -= OnVideoAdPlayed;
    }

    private void OnDataLoaded()
    {
        ShowMoneyAmount();
    }

    private void OnDataUpdated()
    {
        ShowMoneyAmount();
    }

    private void OnBankRobbed()
    {
        ShowRewardAmount();
    }

    private void ShowMoneyAmount()
    {
        _moneyIndicator.text = _playerData.MoneyAmount.ToString();
    }

    private void ShowRewardAmount()
    {
        _rewardMoney.text = PlusSign + _robbery.MoneyRewardAmount;
    }
    
    private void OnVideoAdPlayed()
    {
        _rewardMoney.text = PlusSign + _robbery.MoneyRewardAmount + _robbery.MoneyRewardAmount;
    }
}
