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
    private int[] _aliveRobbers;
    private int _achievedLevels;



    public int Money => _money;
    public int Keys => _keys;
    public int CompletedLevelsCounter => _completedLevelsCounter;
    public int PreviousLevelFloorsAmount => _previousLevelFloorsAmount;
    public int AllMoneyCounter => _allMoneyCounter;
    public int[] AliveRobbers => _aliveRobbers;
    public int AchievedLevels => _achievedLevels;
    

    public Data(int money, int allMoneyCounter, int keys, int completedLevelsCounter, 
        int previousLevelFloorsAmount, int[] aliveRobbers, int achievedLevels)
    {
        _money = money;
        _keys = keys;
        _completedLevelsCounter = completedLevelsCounter;
        _previousLevelFloorsAmount = previousLevelFloorsAmount;
        _allMoneyCounter = allMoneyCounter;
        _aliveRobbers = aliveRobbers;
        _achievedLevels = achievedLevels;
    }
}
