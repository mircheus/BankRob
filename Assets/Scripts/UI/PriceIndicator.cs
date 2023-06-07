using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private EconomicProgression _economicProgression;
    [SerializeField] private PlayerData _playerData;

    private void OnEnable()
    {
        _economicProgression.PriceUpdated += OnPriceUpdated;
        _playerData.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _economicProgression.PriceUpdated -= OnPriceUpdated;
        _playerData.DataLoaded -= OnDataLoaded;
    }

    private void OnPriceUpdated()
    {
        SetPriceText(_economicProgression.CurrentPrice);
    }
    
    private void OnDataLoaded()
    {
        SetPriceText(_economicProgression.CurrentPrice);
    }

    private void SetPriceText(int price)
    {
        _price.text = price.ToString();
    }
}
