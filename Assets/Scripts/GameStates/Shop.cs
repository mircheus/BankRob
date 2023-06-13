using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static System.Net.WebUtility;

public class Shop : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private Grid _grid;
    [SerializeField] private Robber _robberPrefab;
    [SerializeField] private RobbersPool _robbersPool;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EconomicProgression _economicProgression;
    
    [Header("Shop settings")]
    // [SerializeField] private int _robberPrice;
    [SerializeField] private int _poolCapacity;
    
    private List<Robber> _robbers = new List<Robber>();
    private bool IsEnoughMoney => _playerData.MoneyAmount >= _economicProgression.CurrentPrice;

    public event UnityAction NotEnoughMoney;
    public event UnityAction AllSlotsBusy;
    public event UnityAction BuyingRobber;

    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
    }

    private void Start()
    {
        InstantiateRobbers(_poolCapacity);
    }

    public void TryBuyRobber()
    {
        if (IsEnoughMoney)
        {
            if (IsAnySlotAvailable())
            {
                var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
                _robbers.Remove(robber);
                robber.InitializeAsNew();
                PlaceToGrid(robber);
                _playerData.PayForRobber(_economicProgression.CurrentPrice);
                BuyingRobber?.Invoke();
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

    private bool IsAnySlotAvailable()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled == false)
            {
                return true;
            }
        }

        return false;
    }
    
    private void OnDataLoaded()
    {
        PlaceRobbersFromPreviousLevel();
    }

    private void PlaceRobbersFromPreviousLevel()
    {
        int[] robbersFromPreviousLevel = _playerData.AliveRobbers;
        
        if (robbersFromPreviousLevel != null)
        {
            for (int i = 0; i < robbersFromPreviousLevel.Length; i++)
            {
                var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
                robber.SetLevel(robbersFromPreviousLevel[i]);
                robber.SetColor(robber.Level);
                PlaceToGrid(robber);
            } 
            
            _grid.SetRobbersCounter(robbersFromPreviousLevel.Length);
        }
    }
}
