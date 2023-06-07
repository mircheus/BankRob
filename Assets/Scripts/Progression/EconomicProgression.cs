using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicProgression : MonoBehaviour
{
    [Header("Robber price")] 
    [SerializeField] private int _startPrice;
    [SerializeField] private int _priceModifier1;
    [SerializeField] private int _priceModifier2;

    [Header("Reward")] 
    [SerializeField] private int _startReward;
    [SerializeField] private int _rewardModifier1;
    [SerializeField] private int _rewardModifier2;

    private int _currentPrice;
    private int _currentReward;

    public int CurrentReward => _currentReward;
    public int CurrentPrice => _currentPrice;
    
    
}