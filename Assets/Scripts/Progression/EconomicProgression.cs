using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EconomicProgression : MonoBehaviour
{
    [Header("Starter money amount")] [SerializeField]
    private int _starterMoneyAmount;
    
    [Header("Robber price")] 
    [SerializeField] private int _startPrice;
    [SerializeField] private int _priceModifier1;
    [SerializeField] private int _priceModifier2;

    [Header("Reward")] 
    [SerializeField] private int _startReward;
    [SerializeField] private int _rewardModifier1;
    [SerializeField] private int _rewardModifier2;

    [Header("Events sources")] 
    [SerializeField] private Shop _shop;
    [SerializeField] private Robbery _robbery;
    
    [Header("Debug")]
    [SerializeField] private int _currentPrice;
    [SerializeField] private int _currentReward;
    [SerializeField] private int _rewardToNextLevel;


    public event UnityAction PriceUpdated;
    
    public int StartPrice => _startPrice;
    public int StartReward => _startReward;
    public int CurrentReward => _currentReward;
    public int CurrentPrice => _currentPrice;
    public int StarterMoneyAmount => _starterMoneyAmount;
    public int RewardToNextLevel => _rewardToNextLevel;

    private void OnEnable()
    {
        _shop.BuyingRobber += OnBuyingRobber;
        _robbery.BankRobbed += OnBankRobbed;
    }

    private void OnDisable()
    {
        _shop.BuyingRobber -= OnBuyingRobber;
        _robbery.BankRobbed -= OnBankRobbed;
    }

    public void SetCurrentValues(int currentPrice, int currentReward)
    {
        _currentPrice = currentPrice;
        _currentReward = currentReward;
    }
    
    private void OnBuyingRobber()
    {
        _currentPrice += _priceModifier1 + _priceModifier2 * Random.Range(0,1);
        PriceUpdated?.Invoke();
    }

    private void OnBankRobbed()
    {
        _rewardToNextLevel = _currentReward + _rewardModifier1 + _rewardModifier2 * Random.Range(0, 1);
    }
}