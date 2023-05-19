using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Shop _shop;

    private int[] _robbers;

    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
        
        foreach (var slot in _slots)
        {
            slot.RobbersCombined += OnRobbersCombined;
        }
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
        
        foreach (var slot in _slots)
        {
            slot.RobbersCombined -= OnRobbersCombined;
        }
    }

    private void OnDataLoaded()
    {
        FillRobbersFromPreviousLevel(_playerData.AliveRobbers);
    }

    public void OnClick()
    {
        _robbers = _playerData.AliveRobbers;
        FillRobbersFromPreviousLevel(_robbers);
    }

    public void FillRobbersFromPreviousLevel(int[] robbers)
    {
        if (robbers != null)
        {
            for (int i = 0; i < robbers.Length; i++)
            {
                var robber = _shop.Robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
                robber.SetLevel(robbers[i]);
                _slots[i].PlaceNewRobber(robber);
                robber.gameObject.SetActive(true);
            }
        }
    }

    private void OnRobbersCombined()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled && slot.Robber.gameObject.activeSelf == false)
            {
                slot.Unfill();
            }
        }
    }
}
