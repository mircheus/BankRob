using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private Shop _shop;
    
    private int _totalRobbersCounter = 0;

    public int TotalRobbersCounter => _totalRobbersCounter;
    
    public event UnityAction<int> RobbersCombined;
    
    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.RobbersCombined += OnRobbersCombined;
        }

        _shop.BuyingRobber += OnBuyingRobber;
    }

    private void OnDisable()
    {
        foreach (var slot in _slots)
        {
            slot.RobbersCombined -= OnRobbersCombined;
        }
        
        _shop.BuyingRobber += OnBuyingRobber;
    }

    public void SetRobbersCounter(int value)
    {
        _totalRobbersCounter = value;
    }

    private void OnRobbersCombined(int level)
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled && slot.Robber.gameObject.activeSelf == false)
            {
                slot.Unfill();
                _totalRobbersCounter--;
            }
        }
        
        RobbersCombined?.Invoke(level);
    }

    private void OnBuyingRobber()
    {
        _totalRobbersCounter++;
    }
}
