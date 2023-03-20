using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataReflector : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyField;
    [SerializeField] private TMP_Text _keysField;
    // [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private DataManager dataManager;
    
    private void Update()
    {
        // _moneyField.text = Convert.ToString(_playerStats.Money);
        // _keysField.text = Convert.ToString(_playerStats.Keys);
        // _moneyField.text = Convert.ToString(StaticStats.Money);
        // _keysField.text = Convert.ToString(StaticStats.Keys);
        _moneyField.text = Convert.ToString(dataManager.PlayerStats.Money);
        _keysField.text = Convert.ToString(dataManager.PlayerStats.Keys);
    }
}
