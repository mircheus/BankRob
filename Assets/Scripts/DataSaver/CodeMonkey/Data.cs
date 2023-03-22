using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private int _money;
    private int _keys;
    
    public int Money => _money;
    public int Keys => _keys;

    public Data(int money, int keys)
    {
        _money = money;
        _keys = keys;
    }
    
    public void IncrementMoney()
    {
        _money++;
    }

    public void IncrementKeys()
    {
        _keys++;
    }
}
