using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Robbery : MonoBehaviour
{
    [SerializeField] private int _targetQuantity;
    [SerializeField] private List<Safe> _safes = new List<Safe>();

    private int _safesRobbed = 0;

    public event UnityAction BankRobbed;

    private void OnEnable()
    {
        foreach (var safe in _safes)
        {
            safe.Robbed += CountRobbedSafe;
        }
    }

    private void OnDisable()
    {
        foreach (var safe in _safes)
        {
            safe.Robbed -= CountRobbedSafe;
        }
    }

    private void Start()
    {
        _safesRobbed = 0;
    }

    private void CountRobbedSafe()
    {
        _safesRobbed++;
        
        if (IsTargetReached(_safesRobbed))
        {
            BankRobbed?.Invoke();
        }
    }

    private bool IsTargetReached(int currentQuantity)
    {
        return currentQuantity >= _targetQuantity;
    }
}
