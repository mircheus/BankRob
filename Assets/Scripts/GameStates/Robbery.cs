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
    [SerializeField] private PerksPanelFiller _perksPanelFiller;

    [Header("Debug")]
    [SerializeField] private List<Robber> _robbers;
    [SerializeField] private int _aliveRobbersCounter;
    
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
        BankRobbed += OnBankRobbed;
        BankNotRobbed += OnBankNotRobbed;
        
        foreach (var vault in _vaults)
        {
            vault.Robbed += CountRobbedVaults;
        }

        _perksPanelFiller.PerkActivated += OnPerkActivated;
    }

    private void OnDisable()
    {
        BankRobbed -= OnBankRobbed;
        BankNotRobbed -= OnBankNotRobbed;
        
        foreach (var vault in _vaults)
        {
            vault.Robbed -= CountRobbedVaults;
        }

        // foreach (var robber in _robbers)
        // {
        //     robber.GetComponent<RobberMovement>().GetStopped -= OnGetStopped;
        // }
        //
        // _perksPanelFiller.PerkActivated -= OnPerkActivated;
        
    }

    private void Start()
    {
        _robbedVaultsCounter = 0;
        SetTargetsQuantity(_progression.KeysFromPreviousLevel);
    }

    public void AddActiveRobber(Robber robber)
    {
        _robbers.Add(robber);
        robber.GetComponent<RobberMovement>().GetStopped += OnGetStopped;
        _aliveRobbersCounter++;
    }

    public List<Robber> GetRobbersListTo(TargetMovement targetMovement)
    {
        return _robbers;
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
        _aliveRobbersCounter--;

        if (_aliveRobbersCounter == 0)
        {
            BankNotRobbed?.Invoke();
        }
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
}
