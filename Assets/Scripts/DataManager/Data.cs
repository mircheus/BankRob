using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private int _money;
    private int _keys;
    private int _completedLevelsCounter;
    private int _previousLevelFloorsAmount;
    private int _allMoneyCounter;
    
    public int Money => _money;
    public int Keys => _keys;
    public int CompletedLevelsCounter => _completedLevelsCounter;
    public int PreviousLevelFloorsAmount => _previousLevelFloorsAmount;
    public int AllMoneyCounter => _allMoneyCounter;

    public Data(int money, int allMoneyCounter, int keys, int completedLevelsCounter, int previousLevelFloorsAmount)
    {
        _money = money;
        _keys = keys;
        _completedLevelsCounter = completedLevelsCounter;
        _previousLevelFloorsAmount = previousLevelFloorsAmount;
        _allMoneyCounter = allMoneyCounter;
    }
}
