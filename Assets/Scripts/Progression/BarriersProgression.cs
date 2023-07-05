using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BarriersProgression : MonoBehaviour
{
    private const int ColumnsAmount = 4;
    
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Robbery _robbery;

    [Header("Progression settings")] 
    [SerializeField] private int _difficultyFactor;
    [SerializeField] private int _obstaclesReducer;
    [SerializeField] private int _trapsAppearsOnLevel;
    [SerializeField] private int _trapsReducer;

    [Header("Obstacles")] 
    [SerializeField] private Barrier[] _obstacles;

    [Header("Traps")] 
    [SerializeField] private Barrier[] _traps;
    [SerializeField] private int _firstUpgradeOnLevel;
    [SerializeField] private int _secondUpgradeOnLevel;
    [SerializeField] private int _thirdUpgradeOnLevel;

    [Header("First level values")] 
    // [Header("Player settings")]
    // [SerializeField] private int _initMoney;
    [Header("Level settings")]
    [SerializeField] private int _firstLevelFloorsAmount;

    public event UnityAction LevelParametersPrepared;
    public event UnityAction<Barrier[,]> LevelMapPrepared; 
    
    private int _levelsCounter;
    private int _floorsQuantity;
    private int _obstaclesQuantity;
    private int _obstaclesLevel;
    private int _keysQuantity;
    private int _keysFromPreviousLevel;
    private int _trapsLevel;
    private int _trapsQuantity;

    public int FloorsQuantity => _floorsQuantity;
    public int ObstaclesQuantity => _obstaclesQuantity;
    public int ObstaclesLevel => _obstaclesLevel;
    public int KeysQuantity => _keysQuantity;
    public int KeysFromPreviousLevel => _keysFromPreviousLevel;
    public int TrapsQuantity => _trapsQuantity;
    public int TrapsLevel => _trapsLevel;
    // public int InitMoney => _initMoney;
    public int FirstLevelFloorsAmount => _firstLevelFloorsAmount;

    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
    }

    private void OnDataLoaded()
    {
        _levelsCounter = _playerData.CompletedLevelsCounter;
        _floorsQuantity = CalculateFloorsQuantity(_levelsCounter, _playerData.FloorsAmountFromPreviousLevel);
        _trapsQuantity = CalculateTrapsQuantity(_levelsCounter, _floorsQuantity);
        _obstaclesQuantity = CalculateObstaclesQuantity(_floorsQuantity, _trapsQuantity);
        _obstaclesLevel = SetObstaclesLevel(_levelsCounter);
        _trapsLevel = SetTrapsLevel(_levelsCounter);
        LevelMapGenerator levelMapGenerator = new LevelMapGenerator(_obstacles, _traps);
        Barrier[,] levelMap = levelMapGenerator.GetLevelMap(_floorsQuantity, _obstaclesQuantity, _obstaclesLevel, _trapsQuantity, _trapsLevel);
        LevelMapPrepared?.Invoke(levelMap);
    }

    private int CalculateFloorsQuantity(int levelsPassed, int floorsAmountFromPreviousLevel) 
    {
        if (_playerData.IsTryAgain)
        {
            return floorsAmountFromPreviousLevel;
        }
        
        if (levelsPassed % 2 == 0)
        {
            int increasedFloorsAmount = floorsAmountFromPreviousLevel + 1;
            return increasedFloorsAmount;
        }

        return floorsAmountFromPreviousLevel;
    }

    private int CalculateObstaclesQuantity(int currentFloorsAmount, int trapsAmount)
    {
        return (currentFloorsAmount - 1) * _difficultyFactor - trapsAmount - _obstaclesReducer; 
    }

    private int SetObstaclesLevel(int levelsPassed) // DRAFT mechanic of 
    {
        if (levelsPassed < 3)
        {
            return 2;
        }
        else if (levelsPassed >= 3 && levelsPassed < 6 )
        {
            return 3;
        }
        else if (levelsPassed >= 6)
        {
            return 4;
        }

        return -1;
    }

    private int SetKeysQuantity(int levelsPassed) // DRAFT mechanic of progression
    {
        if (levelsPassed >= 3)
        {
            return 2;
        }

        return 0;
    }

    private int CalculateTrapsQuantity(int levelsPassed, int currentFloorsAmount) 
    {
        if (levelsPassed >= _trapsAppearsOnLevel)
        {
            return currentFloorsAmount - _trapsReducer;
        }

        return 0;
    }

    private int SetTrapsLevel(int levelsPassed)
    {
        if (levelsPassed >= _thirdUpgradeOnLevel)
        {
            return 3;
        }
        
        if (levelsPassed >= _secondUpgradeOnLevel)
        {
            return 2;
        }

        if (levelsPassed >= _firstUpgradeOnLevel)
        {
            return 1;
        }

        return 0;
    }
}
