using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsDISABLED : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private int _keysQuantity;

    public int Keys => _keysQuantity;
    public int Money => _money;

    // private void OnDestroy()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }

    public void IncrementKeys()
    {
        _keysQuantity++;
    }

    public void IncrementMoney()
    {
        _money += 100;
    }
}
