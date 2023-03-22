using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataDISABLED
{
    private int _money;
    private int _keys;

    public int Money => _money;
    public int Keys => _keys;
    
    public void IncrementMoney()
    {
        _money += 100;
    }

    public void IncrementKeys()
    {
        _keys += 1;
    }
}
