using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Robbery : MonoBehaviour
{
    [SerializeField] private int _targetQuantity;
    [SerializeField] private List<Vault> _vaults = new List<Vault>();
    
    private int _robbedVaultsCounter = 0;

    public event UnityAction BankRobbed;
    public event UnityAction RobbedVaultsCounterChanged;

    public int TargetQuantity => _targetQuantity;
    public int RobbedVaultsCounter => _robbedVaultsCounter;

    private void OnEnable()
    {
        foreach (var vault in _vaults)
        {
            vault.Robbed += CountRobbedSafe;
        }
    }

    private void OnDisable()
    {
        foreach (var vault in _vaults)
        {
            vault.Robbed -= CountRobbedSafe;
        }
    }

    private void Start()
    {
        _robbedVaultsCounter = 0;
    }

    private void CountRobbedSafe()
    {
        _robbedVaultsCounter++;
        RobbedVaultsCounterChanged?.Invoke();

        if (IsTargetReached(_robbedVaultsCounter))
        {
            BankRobbed?.Invoke();
        }
    }

    private bool IsTargetReached(int currentQuantity)
    {
        return currentQuantity >= _targetQuantity;
    }
}
