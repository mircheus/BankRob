using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Robbery : MonoBehaviour
{
    [SerializeField] private PerksPanelFiller _perksPanelFiller;
    [SerializeField] private EconomicProgression _economicProgression;

    [Header("Debug")]
    [SerializeField] private List<Robber> _robbers;
    [SerializeField] private List<Robber> _aliveRobbers;

    private int _totalRobbersCounter;
    private int _trappedRobbersCounter;
    private int _reachedRobbersCounter;
    private int _robbedVaultsCounter;

    public event UnityAction BankRobbed;
    public event UnityAction BankNotRobbed;
    public event UnityAction RobbedVaultsCounterChanged;
    
    public int MoneyRewardAmount => _economicProgression.CurrentReward * _reachedRobbersCounter;

    private void OnEnable()
    {
        _perksPanelFiller.PerkActivated += OnPerkActivated;
    }

    public void AddActiveRobber(Robber robber)
    {
        _robbers.Add(robber);
        SubscribeToRobber(robber);
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

    public int[] SendRobbersListTo(RobbersSaver robbersSaver)
    {
        int[] array = new int[_robbers.Count];

        for (int i = 0; i < _robbers.Count; i++)
        {
            array[i] = _robbers[i].Level;
        }

        return array;
    }

    public int[] CountAliveRobbers()
    {
        int[] aliveRobbers = new int[_aliveRobbers.Count];

        for (int i = 0; i < _aliveRobbers.Count; i++)
        {
            aliveRobbers[i] = _aliveRobbers[i].Level;
        }

        return aliveRobbers;
    }
    
    private void OnReachedVault(Robber robber)
    {
        UnsubscribeFromRobber(robber);
        RobbedVaultsCounterChanged?.Invoke();
        _aliveRobbers.Add(robber);
        _robbers.Remove(robber);
        _reachedRobbersCounter++;
        CheckRobberyStatus();
    }
    
    private void OnGetStopped()
    {
        _trappedRobbersCounter++;
        
        for (int i = 0; i < _robbers.Count; i++)
        {
            if (_robbers[i].RobberMovement.IsGetStopped)
            {
                UnsubscribeFromRobber(_robbers[i]);
                _robbers.Remove(_robbers[i]);
            }
        }
        
        CheckRobberyStatus();
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

    private void UnsubscribeFromObjects()
    {
        _perksPanelFiller.PerkActivated -= OnPerkActivated;
    }

    private void SubscribeToRobber(Robber robber)
    {
        robber.RobberMovement.GetStopped += OnGetStopped;
        robber.ReachedVault += OnReachedVault;
    }

    private void UnsubscribeFromRobber(Robber robber)
    {
        robber.RobberMovement.GetStopped -= OnGetStopped;
        robber.ReachedVault -= OnReachedVault;
    }

    private void CheckRobberyStatus()
    {
        if (_trappedRobbersCounter + _reachedRobbersCounter == _totalRobbersCounter)
        {
            if (_reachedRobbersCounter > 0)
            {
                BankRobbed?.Invoke();
                UnsubscribeFromObjects();
            }
            else
            {
                BankNotRobbed?.Invoke();
                UnsubscribeFromObjects();
            }
        }
    }
}
