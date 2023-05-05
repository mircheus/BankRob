using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Robbery : MonoBehaviour
{
    [SerializeField] private int _targetQuantity;
    [SerializeField] private int _moneyRewardAmount;
    [SerializeField] private Progression _progression;
    [SerializeField] private List<Vault> _vaults = new List<Vault>();
    
    private int _robbedVaultsCounter;

    public event UnityAction BankRobbed;
    public event UnityAction BankNotRobbed;
    public event UnityAction RobbedVaultsCounterChanged;
    public event UnityAction TargetQuantitySet;

    public int TargetQuantity => _targetQuantity;
    public int MoneyRewardAmount => _moneyRewardAmount * _targetQuantity;
    public int RobbedVaultsCounter => _robbedVaultsCounter;

    private void OnEnable()
    {
        foreach (var vault in _vaults)
        {
            vault.Robbed += CountRobbedVaults;
        }
    }

    private void OnDisable()
    {
        foreach (var vault in _vaults)
        {
            vault.Robbed -= CountRobbedVaults;
        }
    }

    private void Start()
    {
        _robbedVaultsCounter = 0;
        SetTargetsQuantity(_progression.KeysFromPreviousLevel);
    }

    private void SetTargetsQuantity(int keysFromPreviousLevel)
    {
        if (keysFromPreviousLevel == 0)
        {
            _targetQuantity = 1;
        }
        else
        {
            _targetQuantity = 1 + keysFromPreviousLevel;
        }
        
        TargetQuantitySet?.Invoke();
    }

    private void CountRobbedVaults()
    {
        _robbedVaultsCounter++;
        RobbedVaultsCounterChanged?.Invoke();

        if (IsTargetReached(_robbedVaultsCounter))
        {
            BankRobbed?.Invoke();
            Debug.Log("BankRobbed_invoked");
        }
    }

    private bool IsTargetReached(int currentQuantity)
    {
        return currentQuantity == _targetQuantity;
    }
}
