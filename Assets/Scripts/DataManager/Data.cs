using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private int _money;
    private int _keys;
    private int _completedLevelsCounter;

    public int Money => _money;
    public int Keys => _keys;
    public int CompletedLevelsCounter => _completedLevelsCounter;

    public Data(int money, int keys, int completedLevelsCounter)
    {
        _money = money;
        _keys = keys;
        _completedLevelsCounter = completedLevelsCounter;
    }
}
