using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData
{
    private int _money;
    private int _keys;

    public int Money => _money;
    public int Keys => _keys;

    // public PlayerData(PlayerData data)
    // {
    //     _money = data.Money;
    //     _keys = data.Keys;
    // }

    // public PlayerData()
    // {
    //     _money = 0;
    //     _keys = 0;
    // }
    //
    public void IncrementMoney()
    {
        _money += 100;
    }

    public void IncrementKeys()
    {
        _keys += 1;
    }
}
