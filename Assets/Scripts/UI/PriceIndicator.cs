using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceIndicator : MonoBehaviour
{
    private const string PlusOne = "+1";
    
    [SerializeField] private TMP_Text _price;
    [SerializeField] private EconomicProgression _economicProgression;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Shop _shop;
    [SerializeField] private Image _moneyIcon;
    [SerializeField] private Image _adIcon;

    private void OnEnable()
    {
        _economicProgression.PriceUpdated += OnPriceUpdated;
        _playerData.DataLoaded += OnDataLoaded;
        _shop.AllMoneySpent += OnAllMoneySpent;
    }

    private void OnDisable()
    {
        _economicProgression.PriceUpdated -= OnPriceUpdated;
        _playerData.DataLoaded -= OnDataLoaded;
        _shop.AllMoneySpent -= OnAllMoneySpent;
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
    
    private void OnAllMoneySpent()
    {
        _moneyIcon.gameObject.SetActive(false);
        _adIcon.gameObject.SetActive(true);
        _price.text = PlusOne;
    }
}
