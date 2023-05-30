using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VaultsCounter : MonoBehaviour
{
    [SerializeField] private Robbery _robberyInfo;

    private const int Target = 4;
    private TMP_Text _counter;
    private string _counterString;
    private int _currentValue;
    private int _targetValue;

    private void OnEnable()
    {
        _robberyInfo.RobbedVaultsCounterChanged += OnRobbedVaultsCounterChanged;
        // _robberyInfo.TargetQuantitySet += OnTargetQuantitySet;
    }

    private void OnDisable()
    {
        _robberyInfo.RobbedVaultsCounterChanged -= OnRobbedVaultsCounterChanged;
        // _robberyInfo.TargetQuantitySet -= OnTargetQuantitySet;
    }

    private void Start()
    {
        _counter = GetComponent<TMP_Text>();
        _currentValue = 0;
        // _currentValue = _robberyInfo.RobbedVaultsCounter;
        // _targetValue = _robberyInfo.TargetQuantity;
        // SetCounterString(_currentValue);
    }

    // private void OnTargetQuantitySet()
    // {
    //     _currentValue = 0;
    //     // _targetValue = _robberyInfo.TargetQuantity;
    //     SetCounterString(_currentValue);
    // }

    private void OnRobbedVaultsCounterChanged()
    {
        _currentValue++;
        SetCounterString(_currentValue);
    }

    private void SetCounterString(int currentValue)
    {
        string counter =  $"{currentValue} / {Target}";
        _counter.text = counter;
    }
}
