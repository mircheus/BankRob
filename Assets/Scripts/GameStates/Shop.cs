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
    [SerializeField] private Robber _robberPrefab;
    [SerializeField] private RobbersPool _robbersPool;
    [SerializeField] private PlayerData _playerData;
    
    [Header("Shop settings")]
    [SerializeField] private int _robberPrice;
    [SerializeField] private int _poolCapacity;
    
    private List<Robber> _robbers = new List<Robber>();
    private bool IsEnoughMoney => _playerData.MoneyAmount >= _robberPrice;

    public List<Robber> Robbers => _robbers;

    public event UnityAction NotEnoughMoney;
    public event UnityAction AllSlotsBusy;

    private void OnEnable()
    {
        // _playerData.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        // _playerData.DataLoaded -= OnDataLoaded;
    }

    private void Start()
    {
        InstantiateRobbers(_poolCapacity);
        // OnDataLoaded();
    }

    public void TryBuyRobber()
    {
        if (IsEnoughMoney)
        {
            if (IsAnySlotAvailable())
            {
                PlaceRobber();
                _playerData.PayForRobber(_robberPrice);
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

    public Robber GetInitRobber()
    {
        return _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
    }
    
    public void PlaceRobber()
    {
        var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
        
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].IsFilled == false)
            {
                robber.gameObject.SetActive(true);
                _slots[i].PlaceNewRobber(robber);
                break;
            }
        }
    }

    private void InstantiateRobbers(int amount)
    {
        Debug.Log("RobbersInstantiate");
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
        Debug.Log("DataLoaded");
        if (_playerData.AliveRobbers != null)
        {
            for (int i = 0; i < _playerData.AliveRobbers.Length; i++)
            {
                PlaceRobber();
            }
        }
    }
}
