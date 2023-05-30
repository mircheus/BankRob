using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Robbery : MonoBehaviour
{
    [SerializeField] private int _targetQuantity;
    [SerializeField] private int _moneyRewardAmount;
    [SerializeField] private Progression _progression;
    // [SerializeField] private List<Vault> _vaults = new List<Vault>();
    [SerializeField] private PerksPanelFiller _perksPanelFiller;
    [SerializeField] private RobStarter _robStarter;

    [Header("Debug")]
    [SerializeField] private List<Robber> _robbers;
    // [SerializeField] private int _aliveRobbersCounter;
    
    [Header("New Counters")]
    [SerializeField] private int _totalRobbersCounter;
    [SerializeField] private int _trappedRobbersCounter;
    [SerializeField] private int _reachedRobbersCounter;
    
    private int _robbedVaultsCounter;
    // private int _finishedRobbersCounter;

    public event UnityAction BankRobbed;
    public event UnityAction BankNotRobbed;
    public event UnityAction RobbedVaultsCounterChanged;
    public event UnityAction TargetQuantitySet;

    public int TargetQuantity => _targetQuantity;
    public int MoneyRewardAmount => _moneyRewardAmount * _targetQuantity;
    // public int RobbedVaultsCounter => _robbedVaultsCounter;

    private void OnEnable()
    {
        _robStarter.Started += OnStarted;
        BankRobbed += OnBankRobbed;
        BankNotRobbed += OnBankNotRobbed;
        
        // foreach (var vault in _vaults)
        // {
        //     vault.Robbed += CountRobbedVaults;
        // }

        _perksPanelFiller.PerkActivated += OnPerkActivated;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnStarted;
        BankRobbed -= OnBankRobbed;
        BankNotRobbed -= OnBankNotRobbed;
        
        // foreach (var vault in _vaults)
        // {
        //     vault.Robbed -= CountRobbedVaults;
        // }

        // foreach (var robber in _robbers)
        // {
        //     robber.GetComponent<RobberMovement>().GetStopped -= OnGetStopped;
        // }
        //
        // _perksPanelFiller.PerkActivated -= OnPerkActivated;
    }

    private void Start()
    {
        // _robbedVaultsCounter = 0;

        // SetTargetsQuantity(_progression.KeysFromPreviousLevel);
    }

    public void AddActiveRobber(Robber robber)
    {
        _robbers.Add(robber);
        robber.GetComponent<RobberMovement>().GetStopped += OnGetStopped;
        // _aliveRobbersCounter++;
        _totalRobbersCounter++;
    }

    public List<Robber> SendRobbersListTo(TargetMovement targetMovement)
    {
        return _robbers;
    }
    
    public Robber[] SendRobbersListTo(PerksPanel perksPanel)
    {
        Robber[] robbers = new Robber[4];

        for (int i = 0; i < _robbers.Count; i++)
        {
            robbers[_robbers[i].ColumnIndex] = _robbers[i];
        }

        return robbers;
    }

    public int[] CountAliveRobbers()
    {
        int[] aliveRobbers = new int[_robbers.Count];

        for (int i = 0; i < _robbers.Count; i++)
        {
            aliveRobbers[i] = _robbers[i].Level;
        }

        return aliveRobbers;
    }

    // private void SetTargetsQuantity(int keysFromPreviousLevel)
    // {
    //     if (keysFromPreviousLevel == 0)
    //     {
    //         _targetQuantity = 1;
    //     }
    //     else
    //     {
    //         _targetQuantity = 1 + keysFromPreviousLevel;
    //     }
    //     
    //     TargetQuantitySet?.Invoke();
    // }
    
    // private void CountRobbedVaults()
    // {
    //     _robbedVaultsCounter++;
    //     RobbedVaultsCounterChanged?.Invoke();
    //
    //     if (IsTargetReached(_robbedVaultsCounter))
    //     {
    //         // BankRobbed?.Invoke();
    //     }
    // }

    // private bool IsTargetReached(int currentQuantity)
    // {
    //     return currentQuantity == _targetQuantity;
    // }

    private void OnPerkActivated(int index)
    {
        foreach (Robber robber in _robbers)
        {
            if (robber.ColumnIndex == index)
            {
                robber.ActivatePerk();
                break;
            }
        }
    }

    private void OnGetStopped()
    {
        _trappedRobbersCounter++;
        
        for (int i = 0; i < _robbers.Count; i++)
        {
            if (_robbers[i].GetComponent<RobberMovement>().IsGetStopped)
            {
                _robbers[i].ReachedVault -= OnReachedVault;
                _robbers[i].GetComponent<RobberMovement>().GetStopped -= OnGetStopped;
                _robbers.Remove(_robbers[i]);
            }
        }
        
        CheckRobberyStatus();
        
        // _aliveRobbersCounter--;

        // if (_aliveRobbersCounter == 0)
        // {
        //     BankNotRobbed?.Invoke();
        // }
    }

    private void OnBankRobbed()
    {
        UnsubscribeFromObjects();
    }

    private void OnBankNotRobbed()
    {
        UnsubscribeFromObjects();   
    }

    private void UnsubscribeFromObjects()
    {
        foreach (var robber in _robbers)
        {
            robber.GetComponent<RobberMovement>().GetStopped -= OnGetStopped;
        }
        
        _perksPanelFiller.PerkActivated -= OnPerkActivated;
    }

    private void OnStarted()
    {
        foreach (var robber in _robbers)
        {
            robber.ReachedVault += OnReachedVault;
        }
    }

    private void OnReachedVault(Robber robber)
    {
        robber.ReachedVault -= OnReachedVault;
        RobbedVaultsCounterChanged?.Invoke();
        _robbers.Remove(robber);
        _reachedRobbersCounter++;
        
        CheckRobberyStatus();
    }

    private void CheckRobberyStatus()
    {
        if (_trappedRobbersCounter + _reachedRobbersCounter == _totalRobbersCounter)
        {
            if (_reachedRobbersCounter > 0)
            {
                BankRobbed?.Invoke();
            }
            else
            {
                BankNotRobbed?.Invoke();
            }
        }
    }
}
