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
    }

    private void OnDisable()
    {
        _robberyInfo.RobbedVaultsCounterChanged -= OnRobbedVaultsCounterChanged;
    }

    private void Start()
    {
        _counter = GetComponent<TMP_Text>();
        _currentValue = 0;
    }

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
