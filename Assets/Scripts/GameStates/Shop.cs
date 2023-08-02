using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.WebUtility;

public class Shop : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Robber _robberPrefab;
    [SerializeField] private RobbersPool _robbersPool;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EconomicProgression _economicProgression;
    [SerializeField] private AdPlayer _adPlayer;

    [Header("Shop settings")]
    [SerializeField] private int _poolCapacity;
    
    private List<Robber> _robbers = new List<Robber>();
    private bool _isBoughtForAd;
    private bool IsEnoughMoney => _playerData.MoneyAmount >= _economicProgression.CurrentPrice;
    
    public event UnityAction NotEnoughMoney;
    public event UnityAction AllSlotsBusy;
    public event UnityAction BuyingRobber;
    public event UnityAction AllMoneySpent;
    
    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
        // _adPlayer.ExtraRobberGot += OnExtraRobberGot;
    }
    
    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
        // _adPlayer.ExtraRobberGot -= OnExtraRobberGot;
    }

    private void Start()
    {
        InstantiateRobbers(_poolCapacity);
        _isBoughtForAd = false;
    }

    public void TryBuyRobberTEST()
    {
        if (IsEnoughMoney)
        {
            if (_grid.IsAnySlotAvailable())
            {
                GetNewRobber();
                _playerData.PayForRobber(_economicProgression.CurrentPrice);
                BuyingRobber?.Invoke();
                
                // if (IsAllMoneySpent())
                // {
                //     AllMoneySpent?.Invoke();
                // }
            }
            else
            {
                AllSlotsBusy?.Invoke();
            }
        }
        else
        {
            NotEnoughMoney?.Invoke();
        }
    }
    
    public void TryBuyRobber()
    {
        if (_grid.IsAnySlotAvailable())
        {
            if (IsEnoughMoney)
            {
                GetNewRobber();
                _playerData.PayForRobber(_economicProgression.CurrentPrice);
                BuyingRobber?.Invoke();
            }
            else
            {
                NotEnoughMoney?.Invoke();
            }
        }
        else
        {
            AllSlotsBusy?.Invoke();
        }
    }

    public void BuyRobberForAd()
    {
        _adPlayer.VideoAdPlayed += OnVideoAdPlayed;
        _adPlayer.ShowVideoAd();
    }

    private void OnVideoAdPlayed()
    {
        GetNewRobber();
        _adPlayer.VideoAdPlayed -= OnVideoAdPlayed;
    }

    private void GetNewRobber()
    {
        var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
        _robbers.Remove(robber);
        robber.InitializeAsNew();
        PlaceToGrid(robber);
    }
    
    private void PlaceToGrid(Robber robber)
    {
        _grid.PlaceToSlot(robber);
    }

    private void InstantiateRobbers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var robber = Instantiate(_robberPrefab, _robbersPool.transform);
            robber.gameObject.SetActive(false);
            _robbers.Add(robber);
        }
    }

    private void OnDataLoaded()
    {
        PlaceRobbersFromPreviousLevel();
        
        if (IsAllMoneySpent())
        {
            AllMoneySpent?.Invoke();
        }
    }

    private void PlaceRobbersFromPreviousLevel()
    {
        int[] robbersFromPreviousLevel = _playerData.RobbersToSave;
        
        if (robbersFromPreviousLevel != null)
        {
            for (int i = 0; i < robbersFromPreviousLevel.Length; i++)
            {
                var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
                robber.SetLevel(robbersFromPreviousLevel[i]);
                robber.SetColor(robber.Level);
                PlaceToGrid(robber);
            }
        }
    }

    private bool IsAllMoneySpent()
    {
        return _economicProgression.CurrentPrice > _playerData.MoneyAmount;
    }
    
    private void OnExtraRobberGot()
    {
        GetNewRobber();
    }

}
