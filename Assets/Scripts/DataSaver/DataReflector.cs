using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataReflector : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyField;
    [SerializeField] private TMP_Text _keysField;
    [SerializeField] private DataManager dataManager;

    private void OnEnable()
    {
        dataManager.DataUpdated += OnDataUpdated;
    }

    private void OnDisable()
    {
        dataManager.DataUpdated -= OnDataUpdated;
    }

    private void Start()
    {
        _moneyField.text = Convert.ToString(-1);
        _keysField.text = Convert.ToString(-1);
    }
    
    private void OnDataUpdated()
    {
        _moneyField.text = Convert.ToString(dataManager.CurrentData.Money);
        _keysField.text = Convert.ToString(dataManager.CurrentData.Keys);
    }
}
